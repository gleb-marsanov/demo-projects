using System;
using System.Threading.Tasks;
using Data;
using Infrastructure.Bootstrap;
using Services.GameFactory;
using Services.GameTime;
using Services.Progress;
using Services.Ui;
using Services.Ui.Factories;
using Services.Ui.LoadingCurtain;
using StaticData;

namespace Infrastructure.GameStates
{
    public class LoadLevelState : GameState
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private readonly GameConfig _gameConfig;
        private readonly IWindowService _windowService;
        private readonly IUiFactory _uiFactory;
        private readonly IGameTimeService _timeService;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressProvider _progressProvider;
        private readonly ILoadingCurtainService _loadingCurtain;

        public LoadLevelState
        (
            GameStateMachine stateMachine,
            ILoadingCurtainService loadingCurtain,
            SceneLoader sceneLoader,
            GameConfig gameConfig,
            IWindowService windowService,
            IUiFactory uiFactory,
            IGameTimeService timeService,
            IGameFactory gameFactory,
            IProgressProvider progressProvider
        )
        {
            _loadingCurtain = loadingCurtain;
            _sceneLoader = sceneLoader;
            _gameConfig = gameConfig;
            _windowService = windowService;
            _uiFactory = uiFactory;
            _timeService = timeService;
            _gameFactory = gameFactory;
            _progressProvider = progressProvider;
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(_gameConfig.GameplayScene, OnLoaded);
        }

        private async void OnLoaded()
        {
            await LoadGameLevel();
            LoadUi();

            _stateMachine.Enter<GameLoopState>();
        }

        public override void Exit()
        {
            _loadingCurtain.Hide();
        }

        private async Task LoadGameLevel()
        {
            var loadingProgress = 0f;
            _progressProvider.Progress = CreateNewProgress();
            loadingProgress += 0.1f;
            await LoadTheDungeon(loadingProgress);
            loadingProgress += 0.1f;
            await SpawnMonsters(loadingProgress);
            loadingProgress += 0.1f;
            await LoadingDebug(loadingProgress);
        }

        private PlayerProgress CreateNewProgress()
        {
            return new PlayerProgress(
                door: _gameFactory.CreateDoor(),
                gameOverTimer: _gameConfig.GameLoopTime
            );
        }

        private void LoadUi()
        {
            _uiFactory.CreateUiRoot();
            _windowService.ShowGameHud();
        }

        private async Task SpawnMonsters(float loadingProgress)
        {
            _loadingCurtain.SetProgress(loadingProgress, "Spawning monsters");
            await Task.Delay(TimeSpan.FromSeconds(1f));
        }

        private async Task LoadTheDungeon(float loadingProgress)
        {
            _loadingCurtain.SetProgress(loadingProgress, "Entering dungeon");
            await Task.Delay(TimeSpan.FromSeconds(1f));
        }

        private async Task LoadingDebug(float currentProgress)
        {
            const float loadingStep = 0.1f;
            float loadingTime = _gameConfig.LoadingTime * (1 - currentProgress);
            float delay = loadingStep / loadingTime;

            for (float i = 0; i < loadingTime; i += delay)
            {
                await Task.Delay(TimeSpan.FromSeconds(delay));
                float progress = i / loadingTime;
                _loadingCurtain.SetProgress(currentProgress + progress, "Looting chests");
            }
        }
    }
}