using Constants;
using UnityEngine;

namespace Gameplay.Hero
{
    public class GroundChecker : MonoBehaviour
    {
        private enum CheckerType
        {
            Box,
            Circle,
            Ray
        }

        [SerializeField] private bool _drawGizmos = true;
        [SerializeField] private float _size;
        [SerializeField] private float _distance;
        [SerializeField] private Vector2 _origin;
        [SerializeField] private CheckerType _type = CheckerType.Box;

        [field: SerializeField] public bool IsGrounded { get; private set; }
        [field: SerializeField] public int HitsCount { get; private set; }
        public RaycastHit2D[] GroundHits { get; } = new RaycastHit2D[1];

        private LayerMask LayerMask { get; set; }
        private Vector2 Origin => (Vector2)transform.position + _origin;
        private Vector2 Direction { get; } = Vector2.down;
        private Vector2 Size => new Vector2(_size, _size / 2);
        private float DistanceToGround { get; set; }

        private void Start()
        {
            LayerMask = 1 << LayerMask.NameToLayer(Layers.Ground) | 1 << LayerMask.NameToLayer(Layers.Platform);
        }

        private void Update()
        {
            HitsCount = Hits();

            DistanceToGround = HitsCount > 0
                ? transform.position.y - GroundHits[0].point.y
                : _distance;

            IsGrounded = DistanceToGround <= MathConstants.Epsilon && HitsCount > 0;
        }

        private int Hits()
        {
            switch (_type)
            {
                case CheckerType.Box:
                    return BoxHits();

                case CheckerType.Circle:
                    return CircleHits();

                case CheckerType.Ray:
                    return RayHits();

                default:
                    return 0;
            }
        }

        private int RayHits()
        {
            return Physics2D.RaycastNonAlloc(
                origin: Origin,
                direction: Direction,
                results: GroundHits,
                distance: _distance,
                layerMask: LayerMask
            );
        }

        private int CircleHits()
        {
            return Physics2D.CircleCastNonAlloc(
                origin: Origin,
                radius: _size / 2,
                direction: Direction,
                results: GroundHits,
                distance: _distance,
                layerMask: LayerMask
            );
        }

        private int BoxHits()
        {
            return Physics2D.BoxCastNonAlloc(
                origin: Origin,
                size: Size,
                angle: 0,
                direction: Direction,
                results: GroundHits,
                distance: _distance,
                layerMask: LayerMask
            );
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;

            Gizmos.color = IsGrounded ? Color.green : Color.white;
            Vector2 endPoint = Origin + Direction * _distance;
            Gizmos.DrawLine(Origin, endPoint);

            switch (_type)
            {
                case CheckerType.Box:
                    Gizmos.DrawWireCube(endPoint, Size);
                    break;

                case CheckerType.Circle:
                    Gizmos.DrawWireSphere(endPoint, _size / 2);
                    break;
            }
        }
    }
}