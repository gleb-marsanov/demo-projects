namespace Infrastructure.States
{
    internal class GameStateMachineProvider : IGameStateMachineProvider
    {
        public IGameStateMachine ActiveStateMachine { get; set; }
    }
}