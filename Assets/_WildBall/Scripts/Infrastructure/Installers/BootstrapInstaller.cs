using Infrastructure.GameStates.States;
using Infrastructure.GameStates.States.GameStates;
using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.Loading;
using Infrastructure.Services.Providers;
using Infrastructure.Services.Score;
using Infrastructure.Services.Time;
using StaticData;
using Ui;
using Ui.Factories;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindGameStates();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
            Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            Container.Bind<IInputService>().To<KeyboardInputService>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IUiFactory>().To<UiFactory>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();
            Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
            Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.Bind<IGameStateMachineProvider>().To<GameStateMachineProvider>().AsSingle();
        }
    }
}