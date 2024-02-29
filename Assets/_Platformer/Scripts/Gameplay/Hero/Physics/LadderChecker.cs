using System;
using Constants;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Hero.Physics
{
    public class LadderChecker : MonoBehaviour
    {
        [SerializeField] private bool _drawGizmos = true;
        [SerializeField] private Vector2 _origin;
        [SerializeField] private Vector2 _size;

        [field: SerializeField] public bool IsOnLadder { get; private set; }
        
        private LayerMask LayerMask { get; set; }
        private Collider2D[] LadderHits { get; } = new Collider2D[1];
        private Vector2 Origin => (Vector2)transform.position + _origin;
        private Vector2 Size => _size;

        private void Start()
        {
            LayerMask = 1 << LayerMask.NameToLayer(Layers.Ladder);
        }

        private void Update()
        {
            IsOnLadder = Hits() > 0;
        }

        private int Hits()
        {
            return Physics2D.OverlapBoxNonAlloc(
                point: Origin,
                size: Size,
                angle: 0,
                results: LadderHits,
                layerMask: LayerMask
            );
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;
            
            Gizmos.color = IsOnLadder ? Color.green : Color.white;
            Gizmos.DrawWireCube(Origin, Size);
        }
    }
}