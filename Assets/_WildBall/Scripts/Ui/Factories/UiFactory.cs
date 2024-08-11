using StaticData;
using Ui.Elements;
using UnityEngine;
using Zenject;

namespace Ui.Factories
{
    public class UiFactory : IUiFactory
    {
        private GameConfig _gameConfig;
        private IInstantiator _instantiator;
        private Transform _uiRoot;

        [Inject]
        public void Construct(GameConfig gameConfig, IInstantiator instantiator)
        {
            _gameConfig = gameConfig;
            _instantiator = instantiator;
        }
        private Transform UiRoot => _uiRoot ??= new GameObject("UiRoot").transform;

        public MainMenu CreateMainMenu()
        {
            MainMenu prefab = _gameConfig.MainMenuPrefab;
            var instance = _instantiator.InstantiatePrefabForComponent<MainMenu>(prefab, UiRoot);

            foreach (string level in _gameConfig.Levels)
            {
                var button = _instantiator.InstantiatePrefabForComponent<LevelSelectButton>(instance.LevelButtonPrefab, instance.LevelSelectionRoot);
                button.Initialize(level);
            }

            return instance;
        }

        public void Cleanup()
        {
            Object.Destroy(_uiRoot);
            _uiRoot = null;
        }
    }
}