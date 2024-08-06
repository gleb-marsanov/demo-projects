namespace Infrastructure.GameStates
{
    public abstract class GameState : IGameState
    {
        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }
}