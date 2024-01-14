using _SaveTheVillage.Scripts.Constants;
using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.Infrastructure.Loading;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.UI;

namespace _SaveTheVillage.Scripts.Infrastructure.States.GameStates
{
    public class BootstrapState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachineProvider _stateMachineProvider;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IStaticDataService _staticData;
        private readonly ISoundService _soundService;

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public BootstrapState
        (
            ISceneLoader sceneLoader,
            IGameStateMachineProvider stateMachineProvider,
            LoadingCurtain loadingCurtain,
            IStaticDataService staticData,
            ISoundService soundService
        )
        {
            _sceneLoader = sceneLoader;
            _stateMachineProvider = stateMachineProvider;
            _loadingCurtain = loadingCurtain;
            _staticData = staticData;
            _soundService = soundService;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene(Scenes.Bootstrap, OnBootstrapLoaded);
        }

        private void OnBootstrapLoaded()
        {
            _staticData.Load();
            _soundService.PlayClip(SoundId.BackgroundMusic);
            StateMachine.Enter<LoadProgressState>();
        }
    }
}