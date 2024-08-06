using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private TMP_Text _loadingDescription;
        [SerializeField] private CanvasGroup _curtain;
        [SerializeField] private float _fadeSpeed = 0.06f;

        public void SetProgress(float value, string description)
        {
            _progressBar.value = Mathf.Clamp01(value);
            _loadingDescription.text = description;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() =>
            StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= _fadeSpeed;
                yield return new WaitForSeconds(0.03f);
            }

            gameObject.SetActive(false);
        }
    }
}