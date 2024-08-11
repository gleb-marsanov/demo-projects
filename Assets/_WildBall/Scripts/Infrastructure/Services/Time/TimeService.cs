using Infrastructure.GameStates.States;
using Zenject;

namespace Infrastructure.Services.Time
{
    public class TimeService : ITimeService, ITickable
    {
        private readonly IGameStateMachineProvider _stateMachineProvider;

        public TimeService(IGameStateMachineProvider stateMachineProvider)
        {
            _stateMachineProvider = stateMachineProvider;
        }

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public void Tick()
        {
            StateMachine.Update();
        }
    }
}