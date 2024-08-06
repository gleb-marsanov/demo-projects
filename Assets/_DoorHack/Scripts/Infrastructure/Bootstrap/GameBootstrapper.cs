using Infrastructure.GameStates;
using StaticData;
using UnityEngine;

namespace Infrastructure.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private GameConfig _gameConfig;

        private Game _game;

        private void Start()
        {
            _game = new Game(this, _gameConfig);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            _game.StateMachine.Update();
        }
    }
}