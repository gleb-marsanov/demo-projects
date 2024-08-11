using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Input;
using Ui.Factories;

namespace Infrastructure.GameStates.States.GameStates
{
    public class GameLoopState : IExitableState, IUpdatable
    {
        private readonly IInputService _input;
        private readonly IGameFactory _gameFactory;
        private readonly IUiFactory _uiFactory;

        public GameLoopState
        (
            IInputService input,
            IGameFactory gameFactory,
            IUiFactory uiFactory
        )
        {
            _input = input;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _input.Enable();
        }

        public void Exit()
        {
            _input.Disable();
            _gameFactory.Cleanup();
            _uiFactory.Cleanup();
        }

        public void Update()
        {

        }
    }
}