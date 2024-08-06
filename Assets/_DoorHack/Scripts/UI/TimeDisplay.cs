using System;
using Data;
using Services.Progress;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private IProgressProvider _progressProvider;

        public void Construct(IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
        }

        private PlayerProgress Progress => _progressProvider.Progress;

        private void Start()
        {
            Progress.GameOverTimerChanged += OnGameOverTimerChanged;
        }

        private void OnDestroy()
        {
            Progress.GameOverTimerChanged -= OnGameOverTimerChanged;
        }

        private void OnGameOverTimerChanged()
        {
            _text.text = Progress.GameOverTimer.ToString("0");
        }
    }
}