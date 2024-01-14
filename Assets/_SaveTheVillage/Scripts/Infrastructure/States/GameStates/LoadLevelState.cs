using System;
using System.Threading.Tasks;
using _SaveTheVillage.Scripts.Gameplay.Quests;
using _SaveTheVillage.Scripts.Infrastructure.Loading;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using _SaveTheVillage.Scripts.UI;
using _SaveTheVillage.Scripts.UI.Services.Factory;

namespace _SaveTheVillage.Scripts.Infrastructure.States.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameStateMachineProvider _stateMachineProvider;
        private readonly ITimeService _timeService;
        private readonly IUIFactory _uiFactory;
        private readonly IQuestService _questService;

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public LoadLevelState
        (
            ISceneLoader sceneLoader,
            ITimeService timeService,
            LoadingCurtain loadingCurtain,
            IGameStateMachineProvider stateMachineProvider,
            IUIFactory uiFactory,
            IQuestService questService
        )
        {
            _sceneLoader = sceneLoader;
            _timeService = timeService;
            _loadingCurtain = loadingCurtain;
            _stateMachineProvider = stateMachineProvider;
            _uiFactory = uiFactory;
            _questService = questService;
        }

        public void Enter(string sceneName)
        {
            _uiFactory.Cleanup();
            _loadingCurtain.Show();
            InitializeServices();
            _sceneLoader.LoadScene(sceneName, OnSceneLoaded);
        }

        private void InitializeServices()
        {
            _questService.Initialize();
            _timeService.Initialize();
        }

        private async void OnSceneLoaded() =>
            await LoadLevel();

        private async Task LoadLevel()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateHud();

            await Task.Delay(TimeSpan.FromSeconds(2));
            EnterGameLoopState();
        }

        private void EnterGameLoopState()
        {
            StateMachine.Enter<GameLoopState>();
            _loadingCurtain.Hide();
        }
    }

}