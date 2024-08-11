using System;
using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.GameStates.States;
using Infrastructure.GameStates.States.GameStates;
using StaticData;
using Ui;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Gameplay.LevelTransitions
{
    public class LevelEnd : MonoBehaviour
    {
        [SerializeField] private GameObject[] _vfx;
        [SerializeField] private float _delay;

        private IGameStateMachine _gameStateMachine;
        private GameConfig _gameConfig;
        private LoadingCurtain _loadingCurtain;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, GameConfig gameConfig, LoadingCurtain loadingCurtain)
        {
            _loadingCurtain = loadingCurtain;
            _gameConfig = gameConfig;
            _gameStateMachine = gameStateMachine;
        }

        private async void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.HeroTag))
            {
                return;
            }

            await FinishLevel();
        }

        private async UniTask FinishLevel()
        {
            gameObject.SetActive(false);
            foreach (GameObject vfx in _vfx)
                Instantiate(vfx, transform.position, Quaternion.identity);
            await UniTask.Delay(TimeSpan.FromSeconds(_delay));
            _loadingCurtain.SmoothShow(EnterNextLevel);
        }

        private void EnterNextLevel()
        {
            string currentLevelName = SceneManager.GetActiveScene().name;
            int levelIndex = Array.IndexOf(_gameConfig.Levels, currentLevelName);
            bool isLastLevel = levelIndex == _gameConfig.Levels.Length - 1;
            if (isLastLevel)
            {
                _gameStateMachine.Enter<MainMenuState>();
            }
            else
            {
                _gameStateMachine.Enter<LoadLevelState, string>(_gameConfig.Levels[levelIndex + 1]);
            }
        }
    }
}