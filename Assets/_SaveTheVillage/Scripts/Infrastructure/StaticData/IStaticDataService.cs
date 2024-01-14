using System.Collections.Generic;
using _SaveTheVillage.Scripts.StaticData.Battle;
using _SaveTheVillage.Scripts.StaticData.Levels;
using _SaveTheVillage.Scripts.StaticData.Quests;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Timers;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using _SaveTheVillage.Scripts.StaticData.Windows;

namespace _SaveTheVillage.Scripts.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        BattleStaticData BattleStaticData { get; }
        LevelStaticData InitialLevelConfig { get; }
        void Load();
        IEnumerable<VillagerStaticData> GetAllVillagers();
        VillagerStaticData ForVillager(VillagerType villagerType);
        WindowConfig ForWindow(WindowId windowId);
        TimerConfig ForTimer(TimerId timerId);
        QuestStaticData ForQuest(QuestId questId);
        SoundConfig ForSound(SoundId soundId);
    }
}