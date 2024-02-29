using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Input;

namespace Infrastructure.States.GameStates
{
    public class GameLoopState : IExitableState, IUpdatable
    {
        private readonly IInputService _input;
        private readonly IGameFactory _gameFactory;

        public GameLoopState(IInputService input, IGameFactory gameFactory)
        {
            _input = input;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _input.Enable();
        }

        public void Exit()
        {
            _input.Disable();
        }

        public void Update()
        {
        }
    }
}