using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Infrastructure.GameStates.States;
using Infrastructure.GameStates.States.GameStates;
using Infrastructure.Services.Score;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Gameplay.Hero
{
    internal class HeroDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _vfx;
        private LoadingCurtain _loadingCurtain;
        private IGameStateMachine _gameStateMachine;
        private IScoreService _score;

        [Inject]
        public void Construct(LoadingCurtain loadingCurtain, IGameStateMachine gameStateMachine, IScoreService score)
        {
            _score = score;
            _gameStateMachine = gameStateMachine;
            _loadingCurtain = loadingCurtain;
        }

        public async void Die()
        {
            Instantiate(_vfx, transform.position, Quaternion.identity);
            gameObject.SetActive(false);

            await Death();
        }

        private async UniTask Death()
        {
            _score.Reset();
            
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            
            _loadingCurtain.SmoothShow(RestartLevel);
        }

        private void RestartLevel()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);
        }
    }
}