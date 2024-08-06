using Services.Ui;

namespace Infrastructure.GameStates
{
    public class GameOverState : GameState
    {
        private readonly IWindowService _windowService;

        public GameOverState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public override void Enter()
        {
            _windowService.ShowGameOverWindow();
        }
    }
}