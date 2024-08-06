namespace Infrastructure.GameStates
{
    public interface IGameState
    {
        void Enter();
        void Exit();
    }
}