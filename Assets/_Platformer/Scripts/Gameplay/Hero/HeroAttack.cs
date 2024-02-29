using System.Collections;
using Constants;
using Gameplay.Logic.Animations;
using Gameplay.Logic.Interfaces;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Hero
{
    public class HeroAttack : MonoBehaviour
    {
        [SerializeField] private float _comboResetTime = 0.2f;
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _hitRadius = 0.5f;
        [SerializeField] private Vector2 _hitOffset;
        [SerializeField] private HeroAnimator _heroAnimator;
        [SerializeField] private SpriteRenderer _sprite;

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private bool IsAvailable => _heroAnimator.State
            is AnimatorState.Idle
            or AnimatorState.Movement
            or AnimatorState.Jump
            or AnimatorState.Fall;

        private int AttackIndex { get; set; } = 0;

        private Coroutine ResetComboCoroutine { get; set; }

        private Collider2D[] HitColliders { get; } = new Collider2D[5];
        private int AttackDirection => _sprite.flipX ? -1 : 1;
        private Vector2 HitOrigin => (Vector2)transform.position + new Vector2(AttackDirection * _hitOffset.x, _hitOffset.y);
        private LayerMask LayerMask { get; set; }

        private void Start()
        {
            LayerMask = 1 << LayerMask.NameToLayer(Layers.Enemies);
        }

        private void OnEnable()
        {
            _inputService.OnAttackButtonPressed += OnAttackButtonPressed;
            _heroAnimator.OnStateExited += OnStateExited;
        }

        private void OnDisable()
        {
            _inputService.OnAttackButtonPressed -= OnAttackButtonPressed;
            _heroAnimator.OnStateExited -= OnStateExited;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(HitOrigin, _hitRadius);
        }

        private void OnStateExited(AnimatorState state)
        {
            if (state == AnimatorState.Attack)
            {
                ResetComboCoroutine = StartCoroutine(ResetCombo());
            }
        }

        private void OnAttackButtonPressed()
        {
            if (!IsAvailable)
                return;

            if (ResetComboCoroutine != null)
                StopCoroutine(ResetComboCoroutine);

            _heroAnimator.Attack(AttackIndex);

            for (var i = 0; i < Hits(); i++)
                DealDamage(HitColliders[i].gameObject);

            AttackIndex = (AttackIndex + 1) % 2;
        }

        private IEnumerator ResetCombo()
        {
            yield return new WaitForSeconds(_comboResetTime);
            AttackIndex = 0;
        }

        private int Hits()
        {
            return Physics2D.OverlapCircleNonAlloc(
                point: HitOrigin,
                radius: _hitRadius,
                results: HitColliders,
                layerMask: LayerMask
            );
        }

        private void DealDamage(GameObject target)
        {
            if (!target.tag.Equals(Tags.Enemy))
                return;

            var health = target.GetComponent<IHealth>();
            health.TakeDamage(_damage);
        }
    }
}