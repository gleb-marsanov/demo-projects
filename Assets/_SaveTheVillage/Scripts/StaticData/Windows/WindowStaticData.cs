using System.Collections.Generic;
using UnityEngine;

namespace _SaveTheVillage.Scripts.StaticData.Windows
{
    [CreateAssetMenu(menuName = "StaticData/Windows", fileName = "WindowsStaticData")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}