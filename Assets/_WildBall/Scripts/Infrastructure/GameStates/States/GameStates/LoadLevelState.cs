using System;
using Cysharp.Threading.Tasks;
using Gameplay.Cameras;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Loading;
using Infrastructure.Services.Providers;
using Ui;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Infrastructure.GameStates.States.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachineProvider _stateMachineProvider;
        private readonly ISceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IHeroProvider _heroProvider;

        public LoadLevelState
        (
            IGameStateMachineProvider stateMachineProvider,
            ISceneLoader sceneLoader,
            LoadingCurtain loadingCurtain,
            IGameFactory gameFactory,
            IHeroProvider heroProvider
        )
        {
            _stateMachineProvider = stateMachineProvider;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _heroProvider = heroProvider;
        }

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene(sceneName, OnSceneLoaded);
        }

        private async void OnSceneLoaded()
        {
            await InitGameWorld();
            StateMachine.Enter<GameLoopState>();
            _loadingCurtain.Hide();
        }

        private async UniTask InitGameWorld()
        {
            GameObject heroSpawnPoint = GameObject.Find("HeroSpawnPoint");
            GameObject hero = _gameFactory.CreateHero(heroSpawnPoint.transform.position);
            _heroProvider.SetHero(hero);
            var camera = Object.FindObjectOfType<HeroCamera>();
            camera.Initialize(hero);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }
}