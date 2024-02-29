using Constants;
using Gameplay.Logic.Interfaces;
using Infrastructure.Services.Providers;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _timeBetweenAttacks = 1f;
        [SerializeField] private float _damage = 1;
        [SerializeField] private Vector2 _hitBoxSize;
        [SerializeField] private Vector2 _hitBoxOffset;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private EnemyDeath _death;

        private float _lastAttackTime;
        private LayerMask _layerMask;
        private readonly Collider2D[] _hits = new Collider2D[1];

        private IHeroProvider _heroProvider;

        [Inject]
        public void Construct(IHeroProvider heroProvider)
        {
            _heroProvider = heroProvider;
        }

        private void Start()
        {
            _layerMask = 1 << LayerMask.NameToLayer(Layers.Hero);
        }

        private void OnEnable()
        {
            _animator.OnAttack += OnAnimatorAttackEvent;
            _death.Happened += OnDeath;
        }

        private void OnDisable()
        {
            _animator.OnAttack -= OnAnimatorAttackEvent;
            _death.Happened -= OnDeath;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private void Update()
        {
            if (HeroIsInAttackRange() && !IsOnCooldown())
                Attack();
        }

        private void OnAnimatorAttackEvent()
        {
            if (Hits() > 0)
                DealDamage(_hits[0]);
        }

        private void Attack()
        {
            _lastAttackTime = Time.time;
            RotateToPlayer();
            _animator.PlayAttack();
        }

        private void RotateToPlayer()
        {
            transform.rotation = _heroProvider.Hero.transform.position.x < transform.position.x
                ? Quaternion.Euler(0, 180, 0)
                : Quaternion.Euler(0, 0, 0);
        }

        private void DealDamage(Collider2D hit)
        {
            if (!hit.tag.Equals(Tags.Hero))
                return;

            var health = hit.GetComponent<IHealth>();
            health?.TakeDamage(_damage);
        }

        private int Hits()
        {
            return Physics2D.OverlapBoxNonAlloc(
                point: HitBoxPoint(),
                size: _hitBoxSize,
                results: _hits,
                angle: 0,
                layerMask: _layerMask
            );
        }

        private void OnDeath()
        {
            enabled = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.right * _distance);
            Gizmos.DrawWireCube(HitBoxPoint(), _hitBoxSize);
        }

        private bool HeroIsInAttackRange() =>
            Vector2.Distance(transform.position, _heroProvider.Hero.transform.position) < _distance;

        private bool IsOnCooldown() =>
            Time.time - _lastAttackTime < _timeBetweenAttacks;

        private Vector2 HitBoxPoint() =>
            transform.position + transform.right * _hitBoxOffset.x;
    }
}