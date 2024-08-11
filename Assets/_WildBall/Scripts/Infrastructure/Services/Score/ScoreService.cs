using UniRx;
using UnityEngine;

namespace Infrastructure.Services.Score
{
    public class ScoreService : IScoreService
    {
        public IntReactiveProperty Score { get; private set; } = new IntReactiveProperty(0);

        public void AddPoints(int points)
        {
            points = Mathf.Max(0, points);

            Score.Value += points;
        }

        public void Reset()
        {
            Score.Value = 0;
        }
    }
}