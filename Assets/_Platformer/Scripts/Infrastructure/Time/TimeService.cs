using Infrastructure.States;
using Zenject;

namespace Infrastructure.Time
{
    public class TimeService : ITimeService, ITickable
    {
        private readonly IGameStateMachineProvider _gameStateMachineProvider;

        public TimeService(IGameStateMachineProvider gameStateMachineProvider)
        {
            _gameStateMachineProvider = gameStateMachineProvider;
        }

        private IGameStateMachine StateMachine => _gameStateMachineProvider.ActiveStateMachine;

        public void Tick()
        {
            StateMachine.Update();
        }
    }
}