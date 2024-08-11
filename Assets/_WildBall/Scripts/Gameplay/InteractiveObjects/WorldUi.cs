using UnityEngine;

namespace Gameplay.InteractiveObjects
{
    public class WorldUi : MonoBehaviour
    {
        private void Start()
        {
            Camera = Camera.main;
        }

        private Camera Camera { get; set; }

        private void Update()
        {
            transform.forward = Camera.transform.forward;
        }
    }
}