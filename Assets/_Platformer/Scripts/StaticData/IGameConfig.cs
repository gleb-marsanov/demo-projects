using Gameplay.Logic.Triggers;
using UnityEngine;

namespace StaticData
{
    public interface IGameConfig
    {
        string InitialScene { get; }
        GameObject HeroPrefab { get; }
        LevelTransferTrigger LevelTransferTriggerPrefab { get; }
        EnemyConfig[] Enemies { get; }
    }
}