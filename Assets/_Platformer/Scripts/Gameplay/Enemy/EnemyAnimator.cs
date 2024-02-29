using System;
using System.Collections.Generic;
using Constants;
using Gameplay.Logic.Animations;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyDeath _death;
        [SerializeField] private Rigidbody2D _rigidbody;

        private static readonly int DeathStateHash = Animator.StringToHash("death");
        private static readonly int AttackStateHash = Animator.StringToHash("attack");
        private static readonly int IsDeadHash = Animator.StringToHash("is_dead");
        private static readonly int HorizontalVelocityHash = Animator.StringToHash("velocity_x");

        private static readonly Dictionary<int, AnimatorState> States = new Dictionary<int, AnimatorState>
        {
            { DeathStateHash, AnimatorState.Died },
            { AttackStateHash, AnimatorState.Attack },
        };

        public event Action OnAttack;
        public AnimatorState State { get; set; }
        public event Action<AnimatorState> OnStateEntered;
        public event Action<AnimatorState> OnStateExited;

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
            if (_rigidbody.velocity.sqrMagnitude > MathConstants.Epsilon)
                _animator.SetFloat(HorizontalVelocityHash, _rigidbody.velocity.x);
        }

        public void EnteredState(int stateHash)
        {
            State = States.TryGetValue(stateHash, out AnimatorState state)
                ? state
                : AnimatorState.Unknown;

            OnStateEntered?.Invoke(state);
        }

        public void ExitedState(int stateHash)
        {
            OnStateExited?.Invoke(States.TryGetValue(stateHash, out AnimatorState state)
                ? state
                : AnimatorState.Unknown);
        }

        public void PlayAttack() => _animator.Play(AttackStateHash);


        private void OnDeath()
        {
            _animator.SetBool(IsDeadHash, true);
            _animator.Play(DeathStateHash);
        }

        private void OnAttackAnimationEvent()
        {
            OnAttack?.Invoke();
        }
    }
}