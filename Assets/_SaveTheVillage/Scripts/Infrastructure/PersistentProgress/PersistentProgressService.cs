using System.Collections.Generic;
using System.Linq;
using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Data.Quests;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.StaticData.Levels;
using _SaveTheVillage.Scripts.StaticData.Quests;
using _SaveTheVillage.Scripts.StaticData.Timers;
using _SaveTheVillage.Scripts.StaticData.Villagers;

namespace _SaveTheVillage.Scripts.Infrastructure.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private readonly IStaticDataService _staticData;

        public PersistentProgressService(IStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public PlayerProgress Progress { get; private set; }

        public void InitializeNewProgress()
        {
            LevelStaticData levelConfig = _staticData.InitialLevelConfig;

            PlayerBalance villagersData = InitNewPlayerBalance(levelConfig);
            WorldData worldData = InitNewWorldData();
            QuestData questData = InitNewQuestData(levelConfig);

            Progress = new PlayerProgress(villagersData, worldData, questData);
        }

        private static PlayerBalance InitNewPlayerBalance(LevelStaticData levelConfig)
        {
            int wheatCount = levelConfig.InitialWheatCount;
            Dictionary<VillagerType, int> villagers = levelConfig.Villagers.ToDictionary(x => x.Type, x => x.Count);

            return new PlayerBalance(wheatCount, villagers);
        }

        private WorldData InitNewWorldData()
        {
            var harvestingData = new HarvestingData
            (
                harvestingTimer: new TimerData(_staticData.ForTimer(TimerId.Harvesting).Duration),
                totalWheatHarvested: 0
            );
            var feedingData = new FeedingData
            (
                feedingTimer: new TimerData(_staticData.ForTimer(TimerId.Feeding).Duration)
            );
            var battleData = new BattleData
            (
                battleTimer: new TimerData(_staticData.ForTimer(TimerId.Battle).Duration),
                completedBattlesCount: 0,
                nextBattleIndex: 0
            );
            var hireData = new HireData
            (
                hireTime: new Dictionary<VillagerType, float>(),
                totalVillagersHired: 0
            );

            return new WorldData(harvestingData, feedingData, battleData, hireData);
        }

        private QuestData InitNewQuestData(LevelStaticData levelConfig)
        {
            Dictionary<QuestId, QuestProgress> quests = levelConfig.InitialQuests.ToDictionary
            (
                quest => quest.ID,
                quest => new QuestProgress(0, false)
            );

            return new QuestData(quests);
        }
    }
}