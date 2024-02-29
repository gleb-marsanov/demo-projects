using System.Collections.Generic;
using System.Linq;
using Gameplay.Enemy;
using StaticData;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IGameConfig _gameConfig;

        public StaticDataService(IGameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        private Dictionary<EnemyType, EnemyConfig> Enemies { get; set; } = new Dictionary<EnemyType, EnemyConfig>();

        public void Load()
        {
            Enemies = _gameConfig.Enemies.ToDictionary(x => x.EnemyType, x => x);
        }

        public EnemyConfig ForEnemy(EnemyType enemyType)
        {
            return Enemies[enemyType];
        }
    }
}