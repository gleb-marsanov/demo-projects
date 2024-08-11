using Infrastructure.Services.Loading;
using Ui;

namespace Infrastructure.GameStates.States.GameStates
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachineProvider _stateMachineProvider;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;

        public BootstrapState
        (
            IGameStateMachineProvider stateMachineProvider,
            ISceneLoader sceneLoader,
            LoadingCurtain loadingCurtain
        )
        {
            _stateMachineProvider = stateMachineProvider;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene("_Bootstrap", OnBootstrapLoaded);
        }

        private void OnBootstrapLoaded()
        {
            StateMachine.Enter<MainMenuState>();
        }
    }
}