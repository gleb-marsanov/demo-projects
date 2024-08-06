using Data;
using Services.Progress;
using UnityEngine;

namespace Services.GameTime
{
    public class GameTimeService : IGameTimeService
    {
        private readonly IProgressProvider _progressProvider;

        public GameTimeService(IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
        }

        private PlayerProgress Progress => _progressProvider.Progress;

        public void Update()
        {
            float timeLeft = Mathf.Max(0, Progress.GameOverTimer - Time.deltaTime);
            Progress.SetGameOverTimer(timeLeft);
        }
    }
}