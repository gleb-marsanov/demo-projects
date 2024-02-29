using Gameplay.Enemy;
using StaticData;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        EnemyConfig ForEnemy(EnemyType enemyType);
    }
}