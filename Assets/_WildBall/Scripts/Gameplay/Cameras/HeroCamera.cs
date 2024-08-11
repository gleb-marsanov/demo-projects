using Cinemachine;
using UnityEngine;

namespace Gameplay.Cameras
{
    public class HeroCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineFreeLook _cinemachine;

        public void Initialize(GameObject hero)
        {
            _cinemachine.Follow = hero.transform;
            _cinemachine.LookAt = hero.transform;
        }

        public Camera Camera => _camera;
    }
}