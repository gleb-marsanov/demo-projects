using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Hero
{
    public class HeroMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            Camera = Camera.main;
        }

        private Camera Camera { get; set; }
        private Vector2 Axis => _inputService.Axis;

        private void FixedUpdate()
        {
            Vector3 movementVector = Vector3.zero;

            if (Axis.sqrMagnitude > 0.1f)
            {
                movementVector = Camera.transform.TransformDirection(Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                movementVector *= _speed;
            }

            movementVector += Physics.gravity;
            _rigidbody.velocity = movementVector;
        }
    }
}