using Gameplay.Enemy;
using UnityEngine;

namespace Gameplay.SpawnMarkers
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
    }
}