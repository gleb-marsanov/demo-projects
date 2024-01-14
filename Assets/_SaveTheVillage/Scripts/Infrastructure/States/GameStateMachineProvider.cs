namespace _SaveTheVillage.Scripts.Infrastructure.States
{
    internal class GameStateMachineProvider : IGameStateMachineProvider
    {
        public IGameStateMachine ActiveStateMachine { get; set; }
    }
}