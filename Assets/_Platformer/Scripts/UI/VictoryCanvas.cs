using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VictoryCanvas : MonoBehaviour
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        private bool _isActive;

        public event Action OnClose;

        private void Start()
        {
            _canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }

        private void OnDisable()
        {
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClick);
        }

        public void Show()
        {
            if (_isActive)
                return;

            _isActive = true;
            StartCoroutine(ShowCanvas());
        }

        private IEnumerator ShowCanvas()
        {
            var alphaStep = 0.02f;

            while (_canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += alphaStep;
                yield return new WaitForSeconds(alphaStep);
            }
        }

        private void OnMainMenuButtonClick()
        {
            OnClose?.Invoke();
        }
    }
}