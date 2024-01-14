using _SaveTheVillage.Scripts.Gameplay.Battles;
using _SaveTheVillage.Scripts.Gameplay.Feeding;
using _SaveTheVillage.Scripts.Gameplay.GameOver;
using _SaveTheVillage.Scripts.Gameplay.Harvesting;
using _SaveTheVillage.Scripts.Gameplay.Hiring;
using _SaveTheVillage.Scripts.Gameplay.Quests;
using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.Infrastructure.AssetManagement;
using _SaveTheVillage.Scripts.Infrastructure.Loading;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.States;
using _SaveTheVillage.Scripts.Infrastructure.States.GameStates;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using _SaveTheVillage.Scripts.UI;
using _SaveTheVillage.Scripts.UI.Services.Factory;
using _SaveTheVillage.Scripts.UI.Services.Windows;
using UnityEngine;
using Zenject;

namespace _SaveTheVillage.Scripts.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings()
        {
            BindLoadingCurtain();
            BindInfrastructureServices();
            BindGameStates();
            BindGameplayServices();
            BindUIServices();
        }

        private void BindLoadingCurtain()
        {
            Container.BindInterfacesAndSelfTo<LoadingCurtain>().FromComponentInNewPrefab(_loadingCurtain).AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BootstrapInstaller>().FromInstance(this).AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
        }

        private void BindGameStates()
        {
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<LoadProgressState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<QuitGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.Bind<IGameStateMachineProvider>().To<GameStateMachineProvider>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IHarvestingService>().To<HarvestingService>().AsSingle();
            Container.Bind<IBattleService>().To<BattleService>().AsSingle();
            Container.Bind<IFeedingService>().To<FeedingService>().AsSingle();
            Container.Bind<IHireService>().To<HireService>().AsSingle();
            Container.Bind<IQuestService>().To<QuestService>().AsSingle();
            Container.Bind<IGameOverService>().To<GameOverService>().AsSingle();
            Container.Bind<ISoundService>().To<SoundService>().AsSingle();
        }

        private void BindUIServices()
        {
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }
    }
}