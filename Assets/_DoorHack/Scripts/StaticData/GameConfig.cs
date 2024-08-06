using UI;
using UnityEngine;

namespace StaticData
{
    // [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {

        [field: Header("Loading")]
        [field: SerializeField, Range(0, 3f)] public float LoadingTime { get; private set; } = 3f;
        [field: SerializeField] public LoadingCurtain LoadingCurtain { get; private set; }
        
        
        [field: Space(20), Header("Scenes")]
        [field: SerializeField] public string InitialScene { get; private set; } = "Initial";
        [field: SerializeField] public string GameplayScene { get; private set; } = "Gameplay";
        [field: SerializeField, Range(5, 180)] public float GameLoopTime { get; private set; } = 60f;

        
        [field: Space(20), Header("UI")]
        [field: SerializeField] public Canvas UiRoot { get; private set; }
        [field: SerializeField] public GameHud GameHud { get; private set; }
        [field: SerializeField] public GameOverWindow GameOverWindow { get; private set; }
        [field: SerializeField]public VictoryWindow VictoryWindow { get; private set; }


        [field: Space(20), Header("Tools")] 
        [field: SerializeField] public Tool[] Tools { get; private set; }
        [field: SerializeField] public int[] Key { get; private set; } = new[] { 1, 2, 3 };
        [field: SerializeField] public int[] DoorPins { get; set; } = { 5, 5, 5 };
    }
}