using System;
using System.Collections.Generic;
using Constants;
using Gameplay.Logic.Animations;
using UnityEngine;

namespace Gameplay.Hero
{
    public class HeroAnimator : MonoBehaviour, IAnimationStateReader
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private GroundChecker _groundChecker;

        [field: SerializeField] public AnimatorState State { get; private set; }

        public event Action<AnimatorState> OnStateEntered;
        public event Action<AnimatorState> OnStateExited;

        private static readonly int MovementStateHash = Animator.StringToHash("movement");
        private static readonly int DeathStateHash = Animator.StringToHash("death");
        private static readonly int JumpStateHash = Animator.StringToHash("jump");
        private static readonly int DamageStateHash = Animator.StringToHash("take_damage");
        private static readonly int Attack1StateHash = Animator.StringToHash("attack_1");
        private static readonly int Attack2StateHash = Animator.StringToHash("attack_2");

        private static readonly int HorizontalVelocityHash = Animator.StringToHash("velocity_x");
        private static readonly int VerticalVelocityHash = Animator.StringToHash("velocity_y");
        private static readonly int IsGroundedHash = Animator.StringToHash("is_grounded");

        private static readonly Dictionary<int, AnimatorState> StateMap = new Dictionary<int, AnimatorState>
        {
            { MovementStateHash, AnimatorState.Movement },
            { DeathStateHash, AnimatorState.Died },
            { JumpStateHash, AnimatorState.Jump },
            { DamageStateHash, AnimatorState.Damage },
            { Attack1StateHash, AnimatorState.Attack },
            { Attack2StateHash, AnimatorState.Attack },
        };

        private static readonly int[] AttackStates =
        {
            Attack1StateHash,
            Attack2StateHash
        };

        private void Update()
        {
            _animator.SetBool(IsGroundedHash, _groundChecker.HitsCount > 0);
        }

        public void Attack(int attackIndex) => Play(AttackStates[attackIndex]);

        public void EnteredState(int stateHash)
        {
            State = StateMap[stateHash];
            OnStateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            OnStateExited?.Invoke(StateMap[stateHash]);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _animator.SetFloat(HorizontalVelocityHash, velocity.x);
            _animator.SetFloat(VerticalVelocityHash, velocity.y);

            if (Mathf.Abs(velocity.x) > MathConstants.Epsilon)
                _sprite.flipX = velocity.x < 0;
        }

        private void Play(int stateHash)
        {
            AnimatorState nextState = StateMap[stateHash];
            if (State == nextState)
                return;

            _animator.Play(stateHash);
        }

        public void PlayDeath() => _animator.Play(DeathStateHash);
    }

}