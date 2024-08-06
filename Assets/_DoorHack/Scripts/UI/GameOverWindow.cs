using System.Collections;
using Infrastructure.GameStates;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private Button _restartButton;
        [SerializeField, Min(0)] private float _fadeDelay = 0.03f;

        private GameStateMachine _gameStateMachine;

        public void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Start() => 
            _restartButton.onClick.AddListener(OnClickRestart);

        private void OnClickRestart() =>
            _gameStateMachine.Enter<LoadLevelState>();

        public void Show() =>
            StartCoroutine(DoFadeOut());

        private IEnumerator DoFadeOut()
        {
            _curtain.alpha = 0;
            while (_curtain.alpha < 1)
            {
                _curtain.alpha += _fadeDelay;
                yield return new WaitForSeconds(_fadeDelay);
            }
        }
    }
}