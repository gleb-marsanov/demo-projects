using UnityEngine;

namespace _SaveTheVillage.Scripts.StaticData.Timers
{
    [CreateAssetMenu(fileName = "Timers", menuName = "StaticData/Timers", order = 0)]
    public class TimerStaticData : ScriptableObject
    {
        public TimerConfig[] Configs;
    }
}