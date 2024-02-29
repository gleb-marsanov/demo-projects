using Constants;
using UnityEngine;

namespace Gameplay.Hero
{
    public class CeilingChecker : MonoBehaviour
    {
        [SerializeField] private Vector2 _origin;
        [SerializeField] private float _distance;

        [field: SerializeField] public bool IsCeiled { get; private set; }

        private Vector2 Origin => transform.position + (Vector3)_origin;
        private RaycastHit2D[] CeilingHits { get; } = new RaycastHit2D[1];
        private LayerMask LayerMask { get; set; }

        private void Start()
        {
            LayerMask = 1 << LayerMask.NameToLayer(Layers.Ground);
        }

        private void Update()
        {
            IsCeiled = Hits() > 0;
        }

        private int Hits()
        {
            return Physics2D.RaycastNonAlloc(
                origin: Origin,
                direction: Vector2.up,
                results: CeilingHits,
                distance: _distance,
                layerMask: LayerMask
            );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsCeiled ? Color.green : Color.white;
            Gizmos.DrawLine(Origin, Origin + Vector2.up * _distance);
        }
    }
}