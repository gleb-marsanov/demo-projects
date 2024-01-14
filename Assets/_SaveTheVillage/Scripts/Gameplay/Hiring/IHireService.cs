using _SaveTheVillage.Scripts.StaticData.Villagers;

namespace _SaveTheVillage.Scripts.Gameplay.Hiring
{
    public interface IHireService
    {
        bool TryHire(VillagerType type);
        bool CheckHireOpportunity(VillagerType type);
    }
}