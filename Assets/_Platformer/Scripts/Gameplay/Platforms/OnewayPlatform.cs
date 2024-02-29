using System.Collections;
using UnityEngine;

namespace Gameplay.Platforms
{
    [RequireComponent(typeof(Collider2D))]
    public class OnewayPlatform : MonoBehaviour
    {
        private Collider2D[] _colliders;

        private void Start()
        {
            _colliders = GetComponents<Collider2D>();
        }

        public void DisableFor(float duration)
        {
            if (!_colliders[0].enabled)
                return;

            foreach (Collider2D collider in _colliders)
                collider.enabled = false;
            
            StartCoroutine(EnableAfter(duration));
        }

        private IEnumerator EnableAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            foreach (Collider2D collider in _colliders)
                collider.enabled = true;
        }
    }
}