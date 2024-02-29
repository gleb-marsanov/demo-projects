using Infrastructure.Interfaces;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Input;
using Infrastructure.Services.Loading;
using Infrastructure.Services.Providers;
using Infrastructure.Services.StaticData;
using Infrastructure.States;
using Infrastructure.States.GameStates;
using Infrastructure.Time;
using StaticData;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;
        [SerializeField] private GameConfig _config;

        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindGameplayServices();
            BindGameStates();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IInputService>().To<KeyboardInputService>().AsSingle();
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
            Container.Bind<IGameConfig>().FromInstance(_config).AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.Bind<IGameStateMachineProvider>().To<GameStateMachineProvider>().AsSingle();
        }
    }
}