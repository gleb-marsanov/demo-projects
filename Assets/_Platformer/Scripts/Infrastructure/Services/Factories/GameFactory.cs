using Gameplay.Logic.Triggers;
using Gameplay.SpawnMarkers;
using Infrastructure.Services.Providers;
using Infrastructure.Services.StaticData;
using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factories
{
    internal class GameFactory : IGameFactory
    {
        private readonly IGameConfig _gameConfig;
        private readonly IInstantiator _instantiator;
        private Transform _container;
        private readonly IStaticDataService _staticData;
        private readonly IHeroProvider _heroProvider;

        public GameFactory(
            IGameConfig gameConfig,
            IInstantiator instantiator,
            IStaticDataService staticData,
            IHeroProvider heroProvider
        )
        {
            _gameConfig = gameConfig;
            _instantiator = instantiator;
            _staticData = staticData;
            _heroProvider = heroProvider;
        }

        public void Cleanup()
        {
            if (_container != null)
                Object.Destroy(_container.gameObject);

            _container = null;
        }

        private Transform Container => _container ??= new GameObject("GameObjects").transform;

        public GameObject CreateHero(Vector3 at, Quaternion rotation)
        {
            GameObject hero = _instantiator.InstantiatePrefab(_gameConfig.HeroPrefab, at, rotation, Container);
            _heroProvider.SetHero(hero);
            return hero;
        }

        public LevelTransferTrigger CreateLevelTransfer(LevelTransferMarker marker)
        {
            LevelTransferTrigger prefab = _gameConfig.LevelTransferTriggerPrefab;
            var levelTransfer = _instantiator.InstantiatePrefabForComponent<LevelTransferTrigger>(prefab, marker.transform);
            levelTransfer.Initialize(marker.Scene, marker.transform.localScale, marker.Door);
            return levelTransfer;
        }

        public GameObject CreateEnemy(EnemySpawnMarker marker)
        {
            EnemyConfig config = _staticData.ForEnemy(marker.EnemyType);
            GameObject enemy = _instantiator.InstantiatePrefab(config.Prefab, marker.transform.position, Quaternion.identity, Container);
            return enemy;
        }
    }
}