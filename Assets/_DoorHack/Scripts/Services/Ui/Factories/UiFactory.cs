using Infrastructure.GameStates;
using Services.Progress;
using StaticData;
using UI;
using UnityEngine;

namespace Services.Ui.Factories
{
    public class UiFactory : IUiFactory
    {
        private readonly GameConfig _gameConfig;
        private readonly GameStateMachine _gameStateMachine;
        private readonly IProgressProvider _progressProvider;
        private Transform _uiRoot;

        public UiFactory(GameConfig gameConfig, GameStateMachine gameStateMachine, IProgressProvider progressProvider)
        {
            _gameConfig = gameConfig;
            _gameStateMachine = gameStateMachine;
            _progressProvider = progressProvider;
        }

        public UI.LoadingCurtain CreateLoadingCurtain() =>
            Object.Instantiate(_gameConfig.LoadingCurtain);

        public GameHud CreateGameHud()
        {
            GameHud gameHud = Object.Instantiate(_gameConfig.GameHud, _uiRoot);
            gameHud.TimeDisplay.Construct(_progressProvider);
            return gameHud;
        }

        public GameOverWindow CreateGameOverWindow()
        {
            GameOverWindow gameOverWindow = Object.Instantiate(_gameConfig.GameOverWindow, _uiRoot);
            gameOverWindow.Construct(_gameStateMachine);
            return gameOverWindow;
        }

        public void CreateUiRoot() =>
            _uiRoot = Object.Instantiate(_gameConfig.UiRoot).transform;

        public VictoryWindow CreateVictoryWindow()
        {
            VictoryWindow victoryWindow = Object.Instantiate(_gameConfig.VictoryWindow, _uiRoot);
            victoryWindow.Construct(_gameStateMachine);
            return victoryWindow;
        }
    }
}