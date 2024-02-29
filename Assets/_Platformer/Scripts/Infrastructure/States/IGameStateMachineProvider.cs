namespace Infrastructure.States
{
    public interface IGameStateMachineProvider
    {
        IGameStateMachine ActiveStateMachine { get; set; }
    }
}