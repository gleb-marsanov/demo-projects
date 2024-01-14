using UnityEngine;

namespace _SaveTheVillage.Scripts.StaticData.Sounds
{
    [CreateAssetMenu(fileName = "Sounds", menuName = "StaticData/Sounds", order = 0)]
    public class SoundStaticData : ScriptableObject
    {
        public SoundConfig[] Configs;
    }

}