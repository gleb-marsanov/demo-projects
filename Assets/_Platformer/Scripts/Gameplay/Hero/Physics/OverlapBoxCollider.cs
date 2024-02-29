using System;
using UnityEngine;

namespace Gameplay.Hero.Physics
{
    [Serializable]
    public class OverlapBoxCollider : CustomBoxCollider2D
    {
        private readonly Collider2D[] _colliders = new Collider2D[2];

        public int Hits()
        {
            return Physics2D.OverlapBoxNonAlloc(
                point: Origin,
                size: Size,
                angle: 0,
                results: _colliders,
                layerMask: LayerMask
            );
        }
    }
}