namespace _SaveTheVillage.Scripts.Infrastructure.Time
{
    public interface ITimeService
    {
        bool IsPaused { get; }
        float DeltaTime { get; }
        void Initialize();
        void Pause();
        void Unpause();
    }
}