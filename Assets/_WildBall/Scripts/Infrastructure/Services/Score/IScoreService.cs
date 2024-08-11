using UniRx;

namespace Infrastructure.Services.Score
{
    public interface IScoreService
    {
        public IntReactiveProperty Score { get; }
        
        void AddPoints(int points);
        void Reset();
    }

}