using StaticData;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly GameConfig _config;
        private readonly IInstantiator _instantiator;
        private Transform _gameRoot;

        public GameFactory(GameConfig config, IInstantiator instantiator)
        {
            _config = config;
            _instantiator = instantiator;
        }

        private Transform GameRoot => _gameRoot ??= new GameObject("GameRoot").transform;

        public GameObject CreateHero(Vector3 at)
        {
            GameObject prefab = _config.HeroPrefab;
            GameObject hero = _instantiator.InstantiatePrefab(prefab, at, Quaternion.identity, GameRoot);
            return hero;
        }

        public void Cleanup()
        {
            Object.Destroy(GameRoot.gameObject);
            _gameRoot = null;
        }
    }
}