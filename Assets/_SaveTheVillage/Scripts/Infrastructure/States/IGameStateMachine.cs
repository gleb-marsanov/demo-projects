namespace _SaveTheVillage.Scripts.Infrastructure.States
{
    public interface IGameStateMachine : IUpdatable
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}