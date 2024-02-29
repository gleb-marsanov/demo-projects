using Cinemachine;
using UnityEngine;

namespace Gameplay.Logic.Cameras
{
    public class HeroCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachine;

        public void Initialize(Transform hero)
        {
            _cinemachine.Follow = hero;
            _cinemachine.LookAt = hero;
        }
    }
}