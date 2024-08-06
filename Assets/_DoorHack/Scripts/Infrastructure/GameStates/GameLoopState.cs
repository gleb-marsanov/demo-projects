using Data;
using Services.GameTime;
using Services.Progress;

namespace Infrastructure.GameStates
{
    public class GameLoopState : GameState, IUpdatable
    {
        private readonly IProgressProvider _progressProvider;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameTimeService _timeService;

        public GameLoopState(GameStateMachine gameStateMachine, IGameTimeService timeService, IProgressProvider progressProvider)
        {
            _gameStateMachine = gameStateMachine;
            _timeService = timeService;
            _progressProvider = progressProvider;
        }

        private PlayerProgress Progress => _progressProvider.Progress;

        public override void Enter()
        {
            Progress.Door.Opened += OnDoorOpened;
            Progress.GameOverTimerChanged += OnGameOverTimerChanged;
        }

        public override void Exit()
        {
            Progress.Door.Opened -= OnDoorOpened;
            Progress.GameOverTimerChanged -= OnGameOverTimerChanged;
        }

        public void Update()
        {
            _timeService.Update();
        }

        private void OnDoorOpened()
        {
            if (Progress.Door.IsOpen)
                ProcessVictory();
        }

        private void OnGameOverTimerChanged()
        {
            if (Progress.GameOverTimer <= 0)
                ProcessDefeat();
        }

        private void ProcessDefeat()
        {
            _gameStateMachine.Enter<GameOverState>();
        }

        private void ProcessVictory()
        {
            _gameStateMachine.Enter<VictoryState>();
        }
    }
}