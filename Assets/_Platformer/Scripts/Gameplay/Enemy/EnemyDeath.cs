using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private float _destroyDelay = .5f;

        public event Action Happened;

        private void Start()
        {
            _health.CurrentHealth.Subscribe(OnHealthChanged);
        }

        private void OnHealthChanged(float health)
        {
            if (health <= 0)
                Die();
        }

        private void Die()
        {
            Happened?.Invoke();
            StartCoroutine(DestroyAfter(_destroyDelay));
        }

        private IEnumerator DestroyAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}