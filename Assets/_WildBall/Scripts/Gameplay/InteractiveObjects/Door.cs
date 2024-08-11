using Infrastructure.Services.Input;
using UnityEngine;
using Zenject;

namespace Gameplay.InteractiveObjects
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _door;
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _canvas.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Hero"))
                return;

            _inputService.OnInteractionButtonPressed += OpenDoor;
            _canvas.gameObject.SetActive(true);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Hero"))
                return;

            _inputService.OnInteractionButtonPressed -= OpenDoor;
            _canvas.gameObject.SetActive(false);
        }

        private void OpenDoor()
        {
            _door.SetActive(false);
            _canvas.gameObject.SetActive(false);
        }
    }
}