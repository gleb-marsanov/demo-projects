using _SaveTheVillage.Scripts.UI.Elements;
using UnityEngine;

namespace _SaveTheVillage.Scripts.StaticData.Villagers
{
    [CreateAssetMenu(fileName = "Villager", menuName = "StaticData/Villager", order = 0)]
    public class VillagerStaticData : ScriptableObject
    {
        public VillagerType Type;
        public string Name;
        public VillagerCard VillagerCard;
        public int Price;
        public int WheatConsuming;
        public int WheatProducing;
        public float HireDuration;
    }
}