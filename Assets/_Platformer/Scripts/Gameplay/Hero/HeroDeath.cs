using System.Collections;
using Infrastructure.Services.Input;
using Infrastructure.States;
using Infrastructure.States.GameStates;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Gameplay.Hero
{
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private HeroHealth _health;
        private IGameStateMachine _stateMachine;
        private IInputService _inputService;

        [Inject]
        public void Construct(IGameStateMachine stateMachine, IInputService inputService)
        {
            _inputService = inputService;
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _health.CurrentHealth.Subscribe(OnHealthChanged);
        }

        private void OnHealthChanged(float health)
        {
            if (health > 0)
                return;

            _animator.PlayDeath();
            _inputService.Disable();
            StartCoroutine(RestartLevelAfter(2f));
        }

        private IEnumerator RestartLevelAfter(float delay)
        {
            yield return new WaitForSeconds(delay);
            _stateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);
        }

        public void ResetHealthToZero() => 
            _health.CurrentHealth.Value = 0;
    }
}