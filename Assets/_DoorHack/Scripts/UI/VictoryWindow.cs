using System.Collections;
using Infrastructure.GameStates;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VictoryWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private Button _continueButton;
        [SerializeField] private float _fadeSpeed = 0.6f;
        private GameStateMachine _gameStateMachine;

        public void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        private void Start() => 
            _continueButton.onClick.AddListener(RestartGame);

        private void RestartGame() =>
            _gameStateMachine.Enter<LoadLevelState>();

        public void Show() => 
            StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            _canvas.alpha = 0;
            while (_canvas.alpha < 1)
            {
                _canvas.alpha += _fadeSpeed;
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}