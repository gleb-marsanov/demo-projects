using System.Collections.Generic;
using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Data.Quests;
using _SaveTheVillage.Scripts.Extensions;
using _SaveTheVillage.Scripts.Infrastructure.AssetManagement;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.StaticData.Quests;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using _SaveTheVillage.Scripts.StaticData.Windows;
using _SaveTheVillage.Scripts.UI.Elements;
using _SaveTheVillage.Scripts.UI.Hud;
using _SaveTheVillage.Scripts.UI.Windows;
using UnityEngine;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private const string GameHudPath = "UI/GameHud";

        private Transform _uiRoot;
        private GameHud _hud;

        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IInstantiator _instantiator;

        public UIFactory
        (
            IAssetProvider assets,
            IStaticDataService staticData,
            IPersistentProgressService progressService,
            IInstantiator instantiator
        )
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _instantiator = instantiator;
        }

        private PlayerProgress PlayerProgress => _progressService.Progress;

        public void CreateUIRoot()
        {
            var prefab = _assets.Load<Canvas>(UIRootPath);
            Canvas root = Object.Instantiate(prefab);
            _uiRoot = root.transform;
        }

        public void CreateHud()
        {
            var prefab = _assets.Load<GameHud>(GameHudPath);
            var hud = _instantiator.InstantiatePrefabForComponent<GameHud>(prefab, _uiRoot);

            hud.CardsContainer.DestroyAllChildren();
            foreach (VillagerStaticData villager in _staticData.GetAllVillagers())
            {
                var card = _instantiator.InstantiatePrefabForComponent<VillagerCard>(villager.VillagerCard, hud.CardsContainer);
                card.Initialize(villager);
            }

            hud.QuestContainer.DestroyAllChildren();
            foreach (KeyValuePair<QuestId, QuestProgress> quest in PlayerProgress.QuestData.QuestProgress)
            {
                QuestStaticData questConfig = _staticData.ForQuest(quest.Key);
                var questCard = _instantiator.InstantiatePrefabForComponent<QuestCard>(questConfig.QuestCard, hud.QuestContainer);
                questCard.Initialize(questConfig, quest.Value);
            }

            _hud = hud;
        }

        public void Cleanup()
        {
            if (_uiRoot != null)
                Object.Destroy(_uiRoot.gameObject);

            if (_hud != null)
                Object.Destroy(_hud.gameObject);

            _uiRoot = null;
            _hud = null;
        }

        public void CreateWindow(WindowId windowId)
        {
            WindowBase prefab = _staticData.ForWindow(windowId).Template;
            _instantiator.InstantiatePrefab(prefab, _uiRoot);
        }
    }
}