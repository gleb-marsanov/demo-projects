using _SaveTheVillage.Scripts.Infrastructure.States;
using _SaveTheVillage.Scripts.Infrastructure.States.GameStates;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Windows
{
    public class PauseWindow : WindowBase
    {
        [SerializeField] private Button _closeGameButton;
        [SerializeField] private Button _restartButton;

        private IGameStateMachine _gameStateMachine;
        private ITimeService _time;

        [Inject]
        public void Construct
        (
            IGameStateMachine gameStateMachine,
            ITimeService time
        )
        {
            _time = time;
            _gameStateMachine = gameStateMachine;
        }

        protected override void Initialize()
        {
            _time.Pause();
            _restartButton.onClick.AddListener(RestartGame);
            _closeGameButton.onClick.AddListener(CloseGame);
        }

        private void RestartGame()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void CloseGame()
        {
            _gameStateMachine.Enter<QuitGameState>();
        }

        protected override void OnClose()
        {
            _time.Unpause();
        }
    }
}