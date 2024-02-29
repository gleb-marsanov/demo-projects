using System.Collections;
using Cainos.PixelArtPlatformer_Dungeon;
using Constants;
using Infrastructure.Services.Input;
using Infrastructure.States;
using Infrastructure.States.GameStates;
using UnityEngine;
using Zenject;

namespace Gameplay.Logic.Triggers
{
    public class LevelTransferTrigger : MonoBehaviour
    {
        private string _sceneName;
        private bool _isActive = true;
        private Door _door;
        private bool _isTriggered;

        private IGameStateMachine _stateMachine;
        private IInputService _inputService;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IInputService inputService)
        {
            _inputService = inputService;
            _stateMachine = stateMachine;
        }

        public void Initialize(string sceneName, Vector2 scale, Door door)
        {
            _sceneName = sceneName;
            _door = door;
        }

        private void OnEnable()
        {
            _inputService.OnLevelTransferButtonPressed += OnLevelTransferButtonPressed;
        }

        private void OnDisable()
        {
            _inputService.OnLevelTransferButtonPressed -= OnLevelTransferButtonPressed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag.Equals(Tags.Hero))
                _isTriggered = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag.Equals(Tags.Hero))
                _isTriggered = false;
        }

        private void OnLevelTransferButtonPressed()
        {
            if (!_isTriggered || !_isActive)
                return;

            _isActive = false;

            if (_door.IsOpened)
                _door.Close();
            else
                _door.Open();

            _inputService.Disable();
            StartCoroutine(EnterNextLevel());
        }

        private IEnumerator EnterNextLevel()
        {
            yield return new WaitForSeconds(.5f);
            _stateMachine.Enter<LoadLevelState, string>(_sceneName);
        }
    }
}