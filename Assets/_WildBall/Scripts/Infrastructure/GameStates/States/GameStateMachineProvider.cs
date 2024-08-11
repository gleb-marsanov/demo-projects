namespace Infrastructure.GameStates.States
{
    internal class GameStateMachineProvider : IGameStateMachineProvider
    {
        public IGameStateMachine ActiveStateMachine { get; set; }
    }
}