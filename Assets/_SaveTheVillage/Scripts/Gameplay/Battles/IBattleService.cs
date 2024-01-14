using _SaveTheVillage.Scripts.Infrastructure;
using _SaveTheVillage.Scripts.StaticData.Battle;

namespace _SaveTheVillage.Scripts.Gameplay.Battles
{
    public interface IBattleService : IUpdatable
    {
        void StartBattle();
        BattleConfig GetNextBattleConfig();
    }
}