using UnityEngine;

namespace Gameplay.InteractiveObjects
{
    public class ButtonTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject[] _targets;
        [SerializeField] private Transform _child;
        [SerializeField] private float _enabledY;
        [SerializeField] private float _disabledY;
        [SerializeField] private bool _isPressed;

        private void Start()
        {
            UpdateTargets();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Hero"))
                return;

            UpdateTargets();
        }

        private void UpdateTargets()
        {
            _isPressed = !_isPressed;
            foreach (GameObject target in _targets)
            {
                target.SetActive(_isPressed);
            }
            _child.localPosition = new Vector3(_child.localPosition.x, _isPressed ? _enabledY : _disabledY, _child.localPosition.z);
        }
    }
}