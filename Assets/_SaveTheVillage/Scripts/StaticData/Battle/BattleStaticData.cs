using UnityEngine;

namespace _SaveTheVillage.Scripts.StaticData.Battle
{
    [CreateAssetMenu(fileName = "BattleStaticData", menuName = "StaticData/BattleStaticData", order = 0)]
    public class BattleStaticData : ScriptableObject
    {
        public BattleConfig[] Configs;
    }
}