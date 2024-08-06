using Infrastructure.Bootstrap;
using Services;
using Services.GameFactory;
using Services.GameTime;
using Services.Progress;
using Services.Ui;
using Services.Ui.Factories;
using Services.Ui.LoadingCurtain;
using StaticData;

namespace Infrastructure.GameStates
{
    public class BootstrapState : GameState
    {
        private readonly GameConfig _gameConfig;
        private readonly DiContainer _container;
        private readonly SceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine, DiContainer container, SceneLoader sceneLoader, GameConfig gameConfig)
        {
            _container = container;
            _sceneLoader = sceneLoader;
            _gameConfig = gameConfig;
            _stateMachine = stateMachine;

            RegisterServices();
        }

        public override void Enter() =>
            _sceneLoader.Load(_gameConfig.InitialScene, EnterLoadLevelState);

        private void RegisterServices()
        {
            _container.Bind<IProgressProvider>(new ProgressProvider());
            _container.Bind<IGameTimeService>(CreateGameTimeService());
            _container.Bind<IGameFactory>(new GameFactory(_gameConfig));
            _container.Bind<IUiFactory>(CreateUiFactory());
            _container.Bind<ILoadingCurtainService>(CreateLoadingCurtainService());
            _container.Bind<IWindowService>(CreateUiService());
        }

        private GameTimeService CreateGameTimeService()
        {
            return new GameTimeService(_container.Resolve<IProgressProvider>());
        }

        private UiFactory CreateUiFactory()
        {
            return new UiFactory(
                gameConfig: _gameConfig,
                gameStateMachine: _stateMachine,
                progressProvider: _container.Resolve<IProgressProvider>()
            );
        }

        private WindowService CreateUiService()
        {
            return new WindowService(
                gameConfig: _gameConfig,
                uiFactory: _container.Resolve<IUiFactory>(),
                progressProvider: _container.Resolve<IProgressProvider>()
            );
        }

        private LoadingCurtainService CreateLoadingCurtainService() =>
            new LoadingCurtainService(_container.Resolve<IUiFactory>());

        private void EnterLoadLevelState() =>
            _stateMachine.Enter<LoadLevelState>();
    }
}