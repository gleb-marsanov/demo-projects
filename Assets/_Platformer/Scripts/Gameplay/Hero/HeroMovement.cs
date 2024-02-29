using System.Collections;
using Constants;
using Gameplay.Hero.Physics;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _gravity;
        [SerializeField] private AnimationCurve _jumpCurve;
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private CeilingChecker _ceilingChecker;
        [SerializeField] private LadderChecker _ladderChecker;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private HeroAnimator _heroAnimator;

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private bool IsGrounded => _groundChecker.IsGrounded;
        private bool IsCeiled => _ceilingChecker.IsCeiled;
        private bool IsOnLadder => _ladderChecker.IsOnLadder;
        private float Gravity => IsOnLadder || JumpVelocity > Mathf.Epsilon ? 0 : _gravity;
        private Vector2 Axis => _inputService.Axis;
        private float JumpVelocity { get; set; }

        private void OnEnable()
        {
            _inputService.OnJumpButtonPressed += OnJumpButtonPressed;
        }

        private void OnDisable()
        {
            _inputService.OnJumpButtonPressed -= OnJumpButtonPressed;
        }

        private void FixedUpdate()
        {
            Vector2 movementVector = CalculateMovementVector();
            _heroAnimator.SetVelocity(new Vector2(Axis.x, JumpVelocity));
            _rigidbody.AddForce(movementVector * Time.fixedDeltaTime);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, movementVector.magnitude);
        }

        private Vector2 CalculateMovementVector()
        {
            Vector2 movementVector = Vector2.zero;
            bool receivedHorizontalInput = Mathf.Abs(Axis.x) > MathConstants.Epsilon;
            bool receivedVerticalInput = Mathf.Abs(Axis.y) > MathConstants.Epsilon;

            if (receivedHorizontalInput)
                movementVector.x += Axis.x;
            else
                _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);

            if (IsOnLadder && receivedVerticalInput)
                movementVector.y += Axis.y;

            movementVector.Normalize();
            movementVector *= _speed;

            if (!IsGrounded)
                movementVector.y += Gravity;

            if (JumpVelocity > MathConstants.Epsilon)
                movementVector.y += JumpVelocity - Gravity;

            return movementVector;
        }

        private void OnJumpButtonPressed()
        {
            if (IsGrounded || IsOnLadder)
                StartCoroutine(Jump());
        }

        private IEnumerator Jump()
        {
            float time = 0;
            float jumpDuration = _jumpCurve.keys[_jumpCurve.length - 1].time;
            float previousHeight = _jumpCurve.Evaluate(0);

            while (time < jumpDuration && !IsCeiled)
            {
                float deltaTime = Time.fixedDeltaTime;
                float height = _jumpCurve.Evaluate(time);
                float yDelta = height - previousHeight;
                previousHeight = height;

                JumpVelocity = yDelta / deltaTime;

                time += deltaTime;
                yield return new WaitForFixedUpdate();
            }

            JumpVelocity = 0;
        }
    }
}