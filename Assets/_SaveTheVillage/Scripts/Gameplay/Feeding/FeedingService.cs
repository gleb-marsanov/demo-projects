using System.Linq;
using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Gameplay.GameOver;
using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Timers;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Gameplay.Feeding
{
    internal class FeedingService : IFeedingService
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly ITimeService _time;
        private readonly IStaticDataService _staticData;
        private readonly IGameOverService _gameOverService;
        private readonly ISoundService _soundService;

        public FeedingService
        (
            IPersistentProgressService persistentProgress,
            ITimeService time,
            IStaticDataService staticData,
            IGameOverService gameOverService,
            ISoundService soundService
        )
        {
            _persistentProgress = persistentProgress;
            _time = time;
            _staticData = staticData;
            _gameOverService = gameOverService;
            _soundService = soundService;
        }

        private WorldData WorldData => _persistentProgress.Progress.WorldData;
        private PlayerBalance PlayerBalance => _persistentProgress.Progress.PlayerBalance;

        private float TimeUntilFeeding
        {
            get => WorldData.FeedingData.FeedingTimer.Time;
            set => WorldData.FeedingData.FeedingTimer.Time = value;
        }

        public void Update()
        {
            TimeUntilFeeding = Mathf.Max(0, TimeUntilFeeding - _time.DeltaTime);

            if (TimeUntilFeeding > 0)
                return;

            FeedVillagers();
            ResetTimer();
            PlayFeedingSound();
        }

        private void FeedVillagers()
        {
            int totalConsumedWheat = PlayerBalance.Villagers.Sum(GetWheatConsuming);

            if (PlayerBalance.WheatCount < totalConsumedWheat)
                _gameOverService.FinishGame(GameOverReason.NotEnoughFood);

            PlayerBalance.WheatCount -= totalConsumedWheat;
        }

        private int GetWheatConsuming(VillagerType villagerType) =>
            PlayerBalance.GetVillagersCount(villagerType) * _staticData.ForVillager(villagerType).WheatConsuming;

        private void ResetTimer() =>
            TimeUntilFeeding = _staticData.ForTimer(TimerId.Feeding).Duration;

        private void PlayFeedingSound() =>
            _soundService.PlayClip(SoundId.Feeding);
    }
}