using Infrastructure.Bootstrap;
using Infrastructure.GameStates;
using Services;
using StaticData;

namespace Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, GameConfig config)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), DiContainer.Container, config);
        }
    }
}