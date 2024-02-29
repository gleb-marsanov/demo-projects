using Gameplay.Logic.Triggers;
using Gameplay.SpawnMarkers;
using UnityEngine;

namespace Infrastructure.Services.Factories
{
    public interface IGameFactory
    {
        void Cleanup();
        GameObject CreateHero(Vector3 at, Quaternion rotation);
        LevelTransferTrigger CreateLevelTransfer(LevelTransferMarker marker);
        GameObject CreateEnemy(EnemySpawnMarker marker);
    }
}