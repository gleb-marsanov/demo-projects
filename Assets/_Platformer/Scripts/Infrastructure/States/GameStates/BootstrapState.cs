using Constants;
using Infrastructure.Services.Loading;
using Infrastructure.Services.StaticData;
using StaticData;

namespace Infrastructure.States.GameStates
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachineProvider _stateMachineProvider;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameConfig _gameConfig;
        private readonly IStaticDataService _staticData;

        public BootstrapState
        (
            IGameStateMachineProvider stateMachineProvider,
            ISceneLoader sceneLoader,
            IGameConfig gameConfig,
            IStaticDataService staticData
        )
        {
            _stateMachineProvider = stateMachineProvider;
            _sceneLoader = sceneLoader;
            _gameConfig = gameConfig;
            _staticData = staticData;
        }

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public void Enter()
        {
            _sceneLoader.LoadScene(Scenes.Bootstrap, OnBootstrapLoaded);
        }

        private void OnBootstrapLoaded()
        {
            _staticData.Load();
            StateMachine.Enter<LoadLevelState, string>(_gameConfig.InitialScene);
        }
    }
}