using System.Linq;
using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Data.Quests;
using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.StaticData.Quests;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Windows;
using _SaveTheVillage.Scripts.UI.Services.Windows;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Gameplay.Quests
{
    internal class QuestService : IQuestService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly IWindowService _windowService;
        private readonly ISoundService _soundService;

        public QuestService
        (
            IStaticDataService staticDataService,
            IPersistentProgressService persistentProgress,
            IWindowService windowService,
            ISoundService soundService
        )
        {
            _staticDataService = staticDataService;
            _persistentProgress = persistentProgress;
            _windowService = windowService;
            _soundService = soundService;
        }

        private PlayerProgress PlayerProgress => _persistentProgress.Progress;
        private QuestData QuestData => PlayerProgress.QuestData;

        public void Initialize()
        {
            PlayerProgress.WorldData.HireData.OnChange += OnHireDataChange;
            PlayerProgress.WorldData.HarvestingData.OnChange += OnHarvestingDataChange;
            PlayerProgress.WorldData.BattleData.OnChange += OnBattleDataChange;
        }

        private void OnHarvestingDataChange()
        {
            int totalHarvestedWheat = PlayerProgress.WorldData.HarvestingData.TotalWheatHarvested;
            UpdateQuestProgress(QuestId.TrueFarmer, totalHarvestedWheat);
        }

        private void OnHireDataChange()
        {
            int totalVillagersCount = PlayerProgress.WorldData.HireData.TotalVillagersHired;
            UpdateQuestProgress(QuestId.TrueKing, totalVillagersCount);
        }

        private void OnBattleDataChange()
        {
            int completedBattlesCount = PlayerProgress.WorldData.BattleData.CompletedBattlesCount;
            UpdateQuestProgress(QuestId.TrueWarrior, completedBattlesCount);
        }

        private void UpdateQuestProgress(QuestId questId, int currentProgress)
        {

            QuestStaticData questConfig = _staticDataService.ForQuest(questId);
            QuestProgress questProgress = QuestData.QuestProgress[questId];

            if (questProgress.IsCompleted)
                return;

            questProgress.IsCompleted = currentProgress >= questConfig.TargetAmount;
            questProgress.CurrentProgress = Mathf.Min(currentProgress, questConfig.TargetAmount);

            bool allQuestsCompleted = QuestData.QuestProgress.All(x => x.Value.IsCompleted);

            if (allQuestsCompleted)
            {
                _windowService.Open(WindowId.Victory);
                _soundService.PlayClip(SoundId.Victory);
            }
        }
    }
}