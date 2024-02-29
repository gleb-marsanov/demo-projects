using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.Logic.Cameras;
using Gameplay.SpawnMarkers;
using Infrastructure.Services.Factories;
using Infrastructure.Services.Loading;
using UI;
using UnityEngine;
using UnityEngine.Playables;
using Object = UnityEngine.Object;

namespace Infrastructure.States.GameStates
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly IGameStateMachineProvider _stateMachineProvider;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState
        (
            IGameStateMachineProvider stateMachineProvider,
            ISceneLoader sceneLoader,
            IGameFactory gameFactory,
            LoadingCurtain loadingCurtain
        )
        {
            _stateMachineProvider = stateMachineProvider;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _loadingCurtain = loadingCurtain;
        }

        private IGameStateMachine StateMachine => _stateMachineProvider.ActiveStateMachine;

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.LoadScene(sceneName, OnSceneLoaded);
        }

        private async void OnSceneLoaded()
        {
            _gameFactory.Cleanup();
            await PlaySequence();
            await InitGameWorld();
            StateMachine.Enter<GameLoopState>();
        }

        private async UniTask InitGameWorld()
        {
            Debug.Log("InitGameWorld");
            GameObject hero = CreateHero();
            InitHeroCamera(hero);
            CreateLevelTransfers();
            SpawnEnemies();

            await UniTask.Delay(TimeSpan.FromSeconds(.5f));
            _loadingCurtain.Hide();
        }

        private GameObject CreateHero()
        {
            Transform heroSpawnPoint = Object.FindObjectOfType<HeroSpawnPoint>().transform;
            GameObject hero = _gameFactory.CreateHero(heroSpawnPoint.position, Quaternion.identity);
            return hero;
        }

        private static void InitHeroCamera(GameObject hero)
        {
            var heroCamera = Object.FindObjectOfType<HeroCamera>();
            heroCamera.Initialize(hero.transform);
        }

        private void CreateLevelTransfers()
        {
            LevelTransferMarker[] levelTransferMarkers = Object.FindObjectsOfType<LevelTransferMarker>();
            foreach (LevelTransferMarker levelTransferMarker in levelTransferMarkers)
                _gameFactory.CreateLevelTransfer(levelTransferMarker);
        }

        private void SpawnEnemies()
        {
            EnemySpawnMarker[] enemySpawnMarkers = Object.FindObjectsOfType<EnemySpawnMarker>();
            foreach (EnemySpawnMarker marker in enemySpawnMarkers)
                _gameFactory.CreateEnemy(marker);
        }

        private async UniTask PlaySequence()
        {
            var sequence = Object.FindObjectOfType<PlayableDirector>();
            if (sequence == null)
                return;

            _loadingCurtain.Hide();
            sequence.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(sequence.duration));
            Object.Destroy(sequence.gameObject);
            _loadingCurtain.Show();
        }
    }
}