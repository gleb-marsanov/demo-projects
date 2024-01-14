using _SaveTheVillage.Scripts.StaticData.Quests;
using UnityEngine;
using UnityEngine.Serialization;

namespace _SaveTheVillage.Scripts.StaticData.Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "StaticData/Level", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public int InitialWheatCount;
        public VillagersCount[] Villagers;
        [FormerlySerializedAs("ActiveQuests")] public QuestStaticData[] InitialQuests;
    }
}