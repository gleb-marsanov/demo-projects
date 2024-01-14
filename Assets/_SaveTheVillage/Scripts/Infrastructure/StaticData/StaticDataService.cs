using System.Collections.Generic;
using System.Linq;
using _SaveTheVillage.Scripts.Infrastructure.AssetManagement;
using _SaveTheVillage.Scripts.StaticData.Battle;
using _SaveTheVillage.Scripts.StaticData.Levels;
using _SaveTheVillage.Scripts.StaticData.Quests;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Timers;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using _SaveTheVillage.Scripts.StaticData.Windows;

namespace _SaveTheVillage.Scripts.Infrastructure.StaticData
{
    internal class StaticDataService : IStaticDataService
    {
        private const string VillagersDataPath = "StaticData/Villagers";
        private const string TimersDataPath = "StaticData/Timers/Timers";
        private const string StaticDataWindowPath = "StaticData/UI/Windows";
        private const string BattleStaticDataPath = "StaticData/Battle/BattleStaticData";
        private const string QuestsDataPath = "StaticData/Quests";
        private const string InitialLevelConfigPath = "StaticData/Levels/InitialLevel";
        private const string SoundsDataPath = "StaticData/Sound/Sounds";

        private Dictionary<VillagerType, VillagerStaticData> _villagers;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<TimerId, TimerConfig> _timerConfigs;
        private Dictionary<QuestId, QuestStaticData> _questConfigs;
        private Dictionary<SoundId, SoundConfig> _sounds;

        private readonly IAssetProvider _assets;

        public StaticDataService(IAssetProvider assets)
        {
            _assets = assets;
        }

        public BattleStaticData BattleStaticData { get; private set; }
        public LevelStaticData InitialLevelConfig { get; private set; }

        public void Load()
        {
            _villagers = _assets
                .LoadAll<VillagerStaticData>(VillagersDataPath)
                .ToDictionary(x => x.Type, x => x);

            _windowConfigs = _assets
                .Load<WindowStaticData>(StaticDataWindowPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);

            _timerConfigs = _assets
                .Load<TimerStaticData>(TimersDataPath)
                .Configs
                .ToDictionary(x => x.TimerId, x => x);

            _questConfigs = _assets
                .LoadAll<QuestStaticData>(QuestsDataPath)
                .ToDictionary(x => x.ID, x => x);

            _sounds = _assets.Load<SoundStaticData>(SoundsDataPath)
                .Configs
                .ToDictionary(x => x.Id, x => x);

            BattleStaticData = _assets.Load<BattleStaticData>(BattleStaticDataPath);
            InitialLevelConfig = _assets.Load<LevelStaticData>(InitialLevelConfigPath);
        }

        public VillagerStaticData ForVillager(VillagerType villagerType) =>
            _villagers[villagerType];

        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs[windowId];

        public TimerConfig ForTimer(TimerId timerId) =>
            _timerConfigs[timerId];

        public QuestStaticData ForQuest(QuestId questId) =>
            _questConfigs[questId];

        public SoundConfig ForSound(SoundId soundId) =>
            _sounds[soundId];

        public IEnumerable<VillagerStaticData> GetAllVillagers() =>
            _villagers.Values;
    }
}