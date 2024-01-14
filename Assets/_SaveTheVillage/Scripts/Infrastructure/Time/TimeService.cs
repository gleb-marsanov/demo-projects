using _SaveTheVillage.Scripts.Infrastructure.States;
using Zenject;

namespace _SaveTheVillage.Scripts.Infrastructure.Time
{
    internal class TimeService : ITimeService, ITickable
    {
        private readonly IGameStateMachineProvider _stateMachineProvider;

        public TimeService(IGameStateMachineProvider stateMachineProvider)
        {
            _stateMachineProvider = stateMachineProvider;
        }
        
        public float DeltaTime => 
            !IsPaused ? UnityEngine.Time.deltaTime : 0;

        public bool IsPaused { get; private set; }
        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public void Initialize() => 
            Unpause();

        public void Tick()
        {
            if (IsPaused)
                return;

            StateMachine.Update();
        }

        public void Pause() => 
            IsPaused = true;

        public void Unpause() => 
            IsPaused = false;
    }
}