using Infrastructure.Services.Providers;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemy
{
    public class MoveToPlayer : Follow, IMovementDirectionProvider
    {
        [SerializeField] private float _stopDistance = 0.5f;
        [SerializeField] private bool _hasTarget;
        [SerializeField] private bool _isBelowPlayer;
        [SerializeField] private bool _isNextToPlayer;
        [SerializeField] private Vector2 _heroPosition;
        [SerializeField] private Vector2 _position;

        private IHeroProvider _heroProvider;

        [Inject]
        public void Construct(IHeroProvider heroProvider)
        {
            _heroProvider = heroProvider;
        }

        private Transform Hero => _heroProvider.Hero.transform;

        public Vector2 GetMovementDirection()
        {
            _hasTarget = Hero != null;
            _isBelowPlayer = IsBelowPlayer();
            _isNextToPlayer = IsNextToPlayer();

            if (_hasTarget && !_isNextToPlayer)
                return DirectionToPlayer();

            return Vector2.zero;
        }

        private bool IsNextToPlayer()
        {
            _heroPosition = Hero.position;
            _position = transform.position;
            Mathf.Abs(_heroPosition.x - _position.x);

            return Mathf.Abs(Hero.position.x - transform.position.x) <= _stopDistance;
        }

        private bool IsBelowPlayer() =>
            Hero.position.y - transform.position.y > -0.5f;

        private Vector2 DirectionToPlayer()
        {
            return new Vector2(Hero.position.x - transform.position.x, 0).normalized;
        }

    }
}