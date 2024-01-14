namespace _SaveTheVillage.Scripts.Infrastructure.States
{
    public interface IGameStateMachineProvider
    {
        IGameStateMachine ActiveStateMachine { get; set; }
    }
}