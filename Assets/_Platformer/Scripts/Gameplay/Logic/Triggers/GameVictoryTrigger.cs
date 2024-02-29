using System;
using Constants;
using Infrastructure.Services.Input;
using Infrastructure.States;
using UI;
using UnityEngine;
using Zenject;

namespace Gameplay.Logic.Triggers
{
    public class GameVictoryTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private VictoryCanvas _victoryCanvas;
        private bool _isTriggered;

        private IGameStateMachine _gameStateMachine;
        private IInputService _inputService;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IInputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
        }

        private void OnEnable()
        {
            _inputService.OnLevelTransferButtonPressed += StartVictory;
            _victoryCanvas.OnClose += QuitGame;
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;
        }

        private void OnDisable()
        {
            _inputService.OnLevelTransferButtonPressed -= StartVictory;
            _victoryCanvas.OnClose -= QuitGame;
            _triggerObserver.TriggerEnter -= TriggerEnter;
            _triggerObserver.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider2D other)
        {
            if (!other.tag.Equals(Tags.Hero))
                return;

            _isTriggered = true;
        }

        private void TriggerExit(Collider2D obj)
        {
            if (!obj.tag.Equals(Tags.Hero))
                return;

            _isTriggered = false;
        }

        private void StartVictory()
        {
            _victoryCanvas.Show();
            _inputService.Disable();
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}