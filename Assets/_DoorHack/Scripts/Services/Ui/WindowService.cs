using Data;
using Services.Progress;
using Services.Ui.Factories;
using StaticData;
using UI;

namespace Services.Ui
{
    public class WindowService : IWindowService
    {
        private readonly IUiFactory _uiFactory;
        private readonly IProgressProvider _progressProvider;
        private readonly GameConfig _gameConfig;

        private GameHud _gameHud;
        private GameOverWindow _gameOverWindow;
        private VictoryWindow _victoryWindow;

        public WindowService(GameConfig gameConfig, IUiFactory uiFactory, IProgressProvider progressProvider)
        {
            _uiFactory = uiFactory;
            _gameConfig = gameConfig;
            _progressProvider = progressProvider;
        }

        private PlayerProgress Progress => _progressProvider.Progress;

        public void ShowGameHud()
        {
            _gameHud = _uiFactory.CreateGameHud();
            _gameHud.SetDoorPins(Progress.Door.Pin1, Progress.Door.Pin2, Progress.Door.Pin3);
            _gameHud.OnToolUsed += OnToolUsed;
        }

        private void OnToolUsed(int toolIndex)
        {
            Progress.Door.ApplyTool(_gameConfig.Tools[toolIndex]);
            _gameHud.SetDoorPins(Progress.Door.Pin1, Progress.Door.Pin2, Progress.Door.Pin3);
        }

        public void ShowGameOverWindow()
        {
            _gameOverWindow = _uiFactory.CreateGameOverWindow();
            _gameOverWindow.Show();
        }

        public void ShowVictoryWindow()
        {
            _victoryWindow = _uiFactory.CreateVictoryWindow();
            _victoryWindow.Show();
        }
    }
}