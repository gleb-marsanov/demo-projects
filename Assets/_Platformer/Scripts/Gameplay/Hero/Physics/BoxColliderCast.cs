using System;
using UnityEngine;

namespace Gameplay.Hero.Physics
{
    [Serializable]
    public class BoxColliderCast : CustomBoxCollider2D
    {
        [SerializeField] private float _angle;
        [SerializeField] private Vector2 _direction = Vector2.down;
        [SerializeField] private float _distance;

        public RaycastHit2D[] HitPoints { get; } = new RaycastHit2D[2];
        private int _hitsCount;

        public int Hits()
        {
            return _hitsCount = Physics2D.BoxCastNonAlloc(
                origin: Origin,
                size: Size,
                angle: _angle,
                direction: _direction,
                results: HitPoints,
                layerMask: LayerMask,
                distance: _distance
            );
        }

        public override void OnDrawGizmos()
        {
            Vector2 endPoint = _hitsCount == 0 ? Origin + _direction * _distance : HitPoints[0].point;
            Gizmos.DrawLine(Origin, endPoint);
            Gizmos.DrawWireCube(endPoint, Size);
        }
    }
}