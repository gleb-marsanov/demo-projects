using System;
using Cysharp.Threading.Tasks;
using Infrastructure.Services.Loading;
using Infrastructure.Services.Score;
using Ui;
using Ui.Factories;

namespace Infrastructure.GameStates.States.GameStates
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoader _scenes;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IUiFactory _uiFactory;
        private readonly IGameStateMachineProvider _stateMachineProvider;
        private readonly IScoreService _score;

        public MainMenuState
        (
            ISceneLoader scenes,
            LoadingCurtain loadingCurtain,
            IUiFactory uiFactory,
            IGameStateMachineProvider stateMachineProvider,
            IScoreService score
        )
        {
            _scenes = scenes;
            _loadingCurtain = loadingCurtain;
            _uiFactory = uiFactory;
            _stateMachineProvider = stateMachineProvider;
            _score = score;
        }

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public void Enter()
        {
            _loadingCurtain.Show();
            _scenes.LoadScene("MainMenu", LoadMainMenu);
        }

        private async void LoadMainMenu()
        {
            _score.Reset();
            await InitializeScene();
        }

        private async UniTask InitializeScene()
        {
            MainMenu mainMenu = _uiFactory.CreateMainMenu();
            mainMenu.OnLevelSelected += OnLevelSelected;
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _loadingCurtain.Hide();
        }

        private void OnLevelSelected(string levelName)
        {
            StateMachine.Enter<LoadLevelState, string>(levelName);
        }
    }
}