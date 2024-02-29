using System;
using UnityEngine;

namespace Gameplay.Hero.Physics
{
    [Serializable]
    public abstract class CustomBoxCollider2D
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private Vector2 _position;
        [SerializeField] private Collider2D _mainCollider;
        [SerializeField] private LayerMask _layerMask;

        protected Vector2 Origin => _mainCollider.transform.position + (Vector3)_position;
        protected Vector2 Size => _size;
        protected LayerMask LayerMask => _layerMask;

        public virtual void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(Origin, Size);
        }
    }
}