using Services.Ui;

namespace Infrastructure.GameStates
{
    public class VictoryState : GameState
    {
        private readonly IWindowService _windowService;

        public VictoryState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public override void Enter()
        {
            _windowService.ShowVictoryWindow();
        }
    }
}