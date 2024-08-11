using Gameplay.Cameras;
using Ui;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/🛠️GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject HeroPrefab { get; private set; }
        [field: SerializeField] public HeroCamera CameraPrefab { get; private set; }
        [field: SerializeField] public MainMenu MainMenuPrefab { get; private set; }
        [field: SerializeField] public string[] Levels { get; set; }
    }
}