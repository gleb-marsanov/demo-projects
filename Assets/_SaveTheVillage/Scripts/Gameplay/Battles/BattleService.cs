using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Gameplay.GameOver;
using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using _SaveTheVillage.Scripts.StaticData.Battle;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Timers;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using _SaveTheVillage.Scripts.StaticData.Windows;
using _SaveTheVillage.Scripts.UI.Services.Windows;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Gameplay.Battles
{
    internal class BattleService : IBattleService
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly ITimeService _time;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windowService;
        private readonly IGameOverService _gameOverService;
        private readonly ISoundService _soundService;

        public BattleService
        (
            IPersistentProgressService persistentProgress,
            ITimeService time,
            IStaticDataService staticData,
            IWindowService windowService,
            IGameOverService gameOverService, ISoundService soundService)
        {
            _persistentProgress = persistentProgress;
            _time = time;
            _staticData = staticData;
            _windowService = windowService;
            _gameOverService = gameOverService;
            _soundService = soundService;
        }

        private PlayerProgress PlayerProgress => _persistentProgress.Progress;
        private BattleData BattleData => PlayerProgress.WorldData.BattleData;
        private BattleConfig[] BattleConfigs => _staticData.BattleStaticData.Configs;

        private float TimeUntilBattle
        {
            get => BattleData.BattleTimer.Time;
            set => BattleData.BattleTimer.Time = value;
        }

        public void Update()
        {
            TimeUntilBattle = Mathf.Max(0, TimeUntilBattle - _time.DeltaTime);

            if (TimeUntilBattle > 0)
                return;

            OpenBattleWindow();
            ResetTimer();
            _soundService.PlayClip(SoundId.BattleStart);
        }

        public void StartBattle()
        {
            BattleConfig battleConfig = GetNextBattleConfig();
            int playerWarriorsCount = PlayerProgress.PlayerBalance.GetVillagersCount(VillagerType.Warrior);
            int enemiesCount = battleConfig.EnemiesCount;

            if (playerWarriorsCount < enemiesCount)
            {
                _gameOverService.FinishGame(GameOverReason.EnemiesWin);
            }
            else
            {
                BattleData.CompletedBattlesCount++;
                BattleData.NextBattleIndex = Mathf.Min(BattleConfigs.Length - 1, BattleData.NextBattleIndex + 1);

                PlayerProgress.PlayerBalance.RemoveVillagers(VillagerType.Warrior, enemiesCount);
                _soundService.PlayClip(SoundId.BattleWon);
            }
        }

        public BattleConfig GetNextBattleConfig() =>
            BattleConfigs[BattleData.NextBattleIndex];

        private void ResetTimer() =>
            TimeUntilBattle = _staticData.ForTimer(TimerId.Battle).Duration;

        private void OpenBattleWindow() =>
            _windowService.Open(WindowId.Battle);
    }
}