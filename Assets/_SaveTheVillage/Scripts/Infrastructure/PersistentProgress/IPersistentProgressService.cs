using _SaveTheVillage.Scripts.Data;

namespace _SaveTheVillage.Scripts.Infrastructure.PersistentProgress
{
    public interface IPersistentProgressService
    {
        PlayerProgress Progress { get; }
        void InitializeNewProgress();
    }
}