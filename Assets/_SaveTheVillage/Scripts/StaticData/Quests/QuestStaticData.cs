using _SaveTheVillage.Scripts.UI.Elements;
using UnityEngine;

namespace _SaveTheVillage.Scripts.StaticData.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "StaticData/Quest", order = 0)]
    public class QuestStaticData : ScriptableObject
    {
        public QuestId ID;
        public string Description;
        public int TargetAmount;
        public QuestCard QuestCard;
    }
}