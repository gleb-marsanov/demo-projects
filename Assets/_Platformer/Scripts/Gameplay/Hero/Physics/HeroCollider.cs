using UnityEngine;

namespace Gameplay.Hero.Physics
{
    public class HeroCollider : MonoBehaviour
    {
        [SerializeField] private OverlapBoxCollider _groundCollider;
        [SerializeField] private BoxColliderCast _stepCollider;
        [SerializeField] private OverlapBoxCollider _wallCollider;
        [SerializeField] private OverlapBoxCollider _ceilingCollider;

        [field: SerializeField] public bool IsGrounded { get; private set; }
        [field: SerializeField] public bool IsNextToTheWall { get; private set; }
        [field: SerializeField] public bool IsNextToStep { get; private set; }
        [field: SerializeField] public bool IsCeiled { get; private set; }
        public RaycastHit2D[] StepHits => _stepCollider.HitPoints;

        private void FixedUpdate()
        {
            IsGrounded = _groundCollider.Hits() > 0;
            IsNextToTheWall = _wallCollider.Hits() > 0;
            IsNextToStep = _stepCollider.Hits() > 0;
            IsCeiled = _ceilingCollider.Hits() > 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = IsGrounded ? Color.green : Color.white;
            _groundCollider.OnDrawGizmos();

            Gizmos.color = IsNextToTheWall ? Color.green : Color.white;
            _wallCollider.OnDrawGizmos();

            Gizmos.color = IsNextToStep ? Color.green : Color.white;
            _stepCollider.OnDrawGizmos();

            Gizmos.color = IsCeiled ? Color.green : Color.white;
            _ceilingCollider.OnDrawGizmos();
        }
    }
}