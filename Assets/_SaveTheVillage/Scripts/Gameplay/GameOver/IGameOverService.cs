namespace _SaveTheVillage.Scripts.Gameplay.GameOver
{
    public interface IGameOverService
    {
        GameOverReason GameOverReason { get; }
        void FinishGame(GameOverReason reason);
    }
}