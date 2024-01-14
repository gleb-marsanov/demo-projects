using _SaveTheVillage.Scripts.Infrastructure.States;
using _SaveTheVillage.Scripts.Infrastructure.States.GameStates;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Windows
{
    public class VictoryWindow : WindowBase
    {
        [SerializeField] private Button _quitGameButton;
        private IGameStateMachine _gameStateMachine;
        private ITimeService _time;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, ITimeService time)
        {
            _time = time;
            _gameStateMachine = gameStateMachine;
        }

        protected override void Initialize()
        {
            _time.Pause();
            _quitGameButton.onClick.AddListener(CloseGame);
        }

        private void CloseGame()
        {
            _gameStateMachine.Enter<QuitGameState>();
        }

        protected override void OnClose()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }
    }
}