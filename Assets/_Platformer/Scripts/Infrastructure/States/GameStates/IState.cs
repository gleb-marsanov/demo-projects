namespace Infrastructure.States.GameStates
{
    public interface IState : IStateBase
    {
        void Enter();
    }

    public interface IExitableState : IState
    {
        void Exit();
    }

    public interface IPayloadedState<TPayload> : IStateBase
    {
        void Enter(TPayload payload);
    }

    public interface IStateBase
    {
    }
}