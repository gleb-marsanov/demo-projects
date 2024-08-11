namespace Infrastructure.GameStates.States
{
    public interface IGameStateMachineProvider
    {
        IGameStateMachine ActiveStateMachine { get; set; }
    }
}