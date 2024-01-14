using _SaveTheVillage.Scripts.Constants;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;

namespace _SaveTheVillage.Scripts.Infrastructure.States.GameStates
{
    public class LoadProgressState : IState
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IGameStateMachineProvider _stateMachineProvider;

        public LoadProgressState(IPersistentProgressService persistentProgress, IGameStateMachineProvider stateMachineProvider)
        {
            _persistentProgress = persistentProgress;
            _stateMachineProvider = stateMachineProvider;
        }

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public void Enter()
        {
            InitNewProgress();
            EnterLoadLevelState();
        }

        private void InitNewProgress()
        {
            _persistentProgress.InitializeNewProgress();
        }

        private void EnterLoadLevelState()
        {
            StateMachine.Enter<LoadLevelState, string>(Scenes.Gameplay);
        }
    }
}