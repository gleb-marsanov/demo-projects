using Gameplay.Logic.Animations;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class DisableMovementDuringAttack : MonoBehaviour
    {
        [SerializeField] private Follow _follow;
        [SerializeField] private EnemyRotation _rotation;
        [SerializeField] private EnemyAnimator _animator;

        private void OnEnable()
        {
            _animator.OnStateEntered += OnStateEntered;
            _animator.OnStateExited += OnStateExited;
        }

        private void OnDisable()
        {
            _animator.OnStateEntered -= OnStateEntered;
            _animator.OnStateExited -= OnStateExited;
        }

        private void OnStateEntered(AnimatorState state)
        {
            if (state != AnimatorState.Attack)
                return;

            _rotation.enabled = false;
            _follow.enabled = false;
        }

        private void OnStateExited(AnimatorState state)
        {
            if (state != AnimatorState.Attack)
                return;

            _rotation.enabled = true;
            _follow.enabled = true;
        }
    }
}