using Gameplay.Logic.Interfaces;
using UniRx;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float _maxHealth;

        public ReactiveProperty<float> CurrentHealth { get; } = new ReactiveProperty<float>();

        private void Start()
        {
            CurrentHealth.Value = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth.Value <= 0)
                return;

            damage = Mathf.Max(0, damage);
            CurrentHealth.Value = Mathf.Max(0, CurrentHealth.Value - damage);
        }
    }
}