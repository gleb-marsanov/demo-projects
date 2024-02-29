using Cainos.PixelArtPlatformer_Dungeon;
using UnityEngine;

namespace Gameplay.SpawnMarkers
{
    public class LevelTransferMarker : MonoBehaviour
    {
        [field: SerializeField] public string Scene { get; private set; }
        [field: SerializeField] public Door Door { get; private set; }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, transform.localScale);
            GUI.Label(new Rect(transform.position, new Vector2(200, 50)), Scene);
        }
    }
}