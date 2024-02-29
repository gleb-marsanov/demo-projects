using Constants;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Vector2 _velocity;
        [SerializeField] private EnemyDeath _death;
        [SerializeField] private Collider2D _collider;

        private IMovementDirectionProvider[] _velocityProviders;
        private RaycastHit2D[] _groundHits = new RaycastHit2D[1];
        private LayerMask _groundLayerMask;

        private void Start()
        {
            _groundLayerMask = 1 << LayerMask.NameToLayer(Layers.Ground) | 1 << LayerMask.NameToLayer(Layers.Platform);
            _velocityProviders = GetComponents<IMovementDirectionProvider>();
        }

        private void OnEnable()
        {
            _death.Happened += OnDeath;
        }

        private void OnDisable()
        {
            _death.Happened -= OnDeath;
        }

        private void Update()
        {
            _velocity = Vector2.zero;

            foreach (IMovementDirectionProvider velocityProvider in _velocityProviders)
            {
                if (velocityProvider.enabled)
                    _velocity += velocityProvider.GetMovementDirection();
            }

            _velocity *= _speed;

            if (!IsGrounded())
                _velocity.y += _rigidbody.gravityScale * Physics2D.gravity.y;

            if (Mathf.Abs(_velocity.x) < MathConstants.Epsilon)
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);


            _rigidbody.AddForce(_velocity, ForceMode2D.Force);
            _rigidbody.velocity = _rigidbody.velocity.magnitude > _velocity.magnitude
                ? Vector2.ClampMagnitude(_rigidbody.velocity, _velocity.magnitude)
                : _velocity;
        }

        private bool IsGrounded()
        {
            float radius = _collider.bounds.extents.x - 0.01f;
            var origin = transform.position + Vector3.up * radius;

            int hitsCount = Physics2D.CircleCastNonAlloc(
                origin: origin,
                radius: radius,
                direction: Vector2.down,
                results: _groundHits,
                distance: 1f,
                layerMask: _groundLayerMask
            );

            if (hitsCount > 0)
            {
                return Vector2.Distance(origin, _groundHits[0].point) < radius + 0.05f;
            }

            return false;
        }

        private void OnDeath()
        {
            enabled = false;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}