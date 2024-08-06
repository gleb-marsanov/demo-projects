using System;

namespace Data
{
    public class PlayerProgress
    {
        public PlayerProgress(Door door, float gameOverTimer)
        {
            Door = door;
            GameOverTimer = gameOverTimer;
        }

        public event Action GameOverTimerChanged;
        public Door Door { get; private set; }
        public float GameOverTimer { get; private set; }

        public void SetGameOverTimer(float value)
        {
            GameOverTimer = value;
            GameOverTimerChanged?.Invoke();
        }
    }
}