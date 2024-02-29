using Constants;
using Gameplay.Platforms;
using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.Hero
{
    public class HeroPlatformDismount : MonoBehaviour
    {
        [SerializeField] private bool _drawGizmos = true;
        [SerializeField] private float _radius;
        [SerializeField] private Vector2 _origin;
        [SerializeField] private float _distance;

        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private RaycastHit2D[] PlatformHits { get; } = new RaycastHit2D[1];
        private LayerMask PlatformLayer { get; set; }
        private Vector2 Origin => (Vector2)transform.position + _origin;

        private void Start()
        {
            PlatformLayer = 1 << LayerMask.NameToLayer(Layers.Platform);
        }

        private void OnEnable()
        {
            _inputService.OnDismountButtonPressed += OnDismountButtonPressed;
        }

        private void OnDisable()
        {
            _inputService.OnDismountButtonPressed -= OnDismountButtonPressed;
        }

        private void OnDismountButtonPressed()
        {
            if (Hits() <= 0)
                return;

            if (PlatformHits[0].collider.TryGetComponent(out OnewayPlatform onewayPlatform))
                onewayPlatform.DisableFor(.3f);
        }

        private int Hits()
        {
            return Physics2D.CircleCastNonAlloc(
                origin: Origin,
                radius: _radius,
                direction: Vector2.down,
                results: PlatformHits,
                distance: _distance,
                layerMask: PlatformLayer
            );
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;

            Gizmos.color = Hits() > 0 ? Color.green : Color.white;
            Vector2 endPoint = Origin + Vector2.down * _distance;
            Gizmos.DrawWireSphere(endPoint, _radius);
            Gizmos.DrawLine(Origin, endPoint);
        }
    }
}