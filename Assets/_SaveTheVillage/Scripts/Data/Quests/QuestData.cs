using System.Collections.Generic;
using _SaveTheVillage.Scripts.StaticData.Quests;

namespace _SaveTheVillage.Scripts.Data.Quests
{
    public class QuestData
    {
        public readonly Dictionary<QuestId, QuestProgress> QuestProgress;

        public QuestData(Dictionary<QuestId, QuestProgress> questProgress)
        {
            QuestProgress = questProgress;
        }
    }
}