using Gameplay.Logic.Triggers;
using UnityEngine;

namespace StaticData
{

    [CreateAssetMenu(fileName = "GameConfig", menuName = "StaticData/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject, IGameConfig
    {
        [field: SerializeField] public string InitialScene { get; private set; }
        [field: SerializeField] public GameObject HeroPrefab { get; private set; }
        [field: SerializeField] public LevelTransferTrigger LevelTransferTriggerPrefab { get; private set; }
        [field: SerializeField] public EnemyConfig[] Enemies { get; private set; }
    }
}