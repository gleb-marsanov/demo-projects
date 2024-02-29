using Constants;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyRotation : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private EnemyDeath _death;
        
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
            if (!IsMoving())
                return;

            transform.rotation = IsMovingToRight()
                ? Quaternion.Euler(0, 0, 0)
                : Quaternion.Euler(0, 180, 0);
        }
        
        private void OnDeath()
        {
            enabled = false;
        }
        
        private bool IsMovingToRight() =>
            _rigidbody.velocity.x > 0;

        private bool IsMoving() =>
            Mathf.Abs(_rigidbody.velocity.x) > MathConstants.Epsilon;
    }
}