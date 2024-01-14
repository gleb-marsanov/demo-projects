using System.Linq;
using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Timers;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using UnityEngine;
using UnityEngine.Playables;

namespace _SaveTheVillage.Scripts.Gameplay.Harvesting
{
    internal class HarvestingService : IHarvestingService
    {
        private readonly IPersistentProgressService _persistentProgress;
        private readonly ITimeService _time;
        private readonly IStaticDataService _staticData;
        private readonly ISoundService _soundService;

        public HarvestingService
        (
            IPersistentProgressService persistentProgress,
            ITimeService time,
            IStaticDataService staticData,
            ISoundService soundService
        )
        {
            _persistentProgress = persistentProgress;
            _time = time;
            _staticData = staticData;
            _soundService = soundService;
        }

        private PlayerBalance PlayerBalance => _persistentProgress.Progress.PlayerBalance;
        private WorldData WorldData => _persistentProgress.Progress.WorldData;

        private float TimeUntilHarvesting
        {
            get => WorldData.HarvestingData.HarvestingTimer.Time;
            set => WorldData.HarvestingData.HarvestingTimer.Time = value;
        }

        public void Update()
        {
            TimeUntilHarvesting = Mathf.Max(0, TimeUntilHarvesting - _time.DeltaTime);

            if (TimeUntilHarvesting > 0)
                return;

            CollectWheat();
            ResetTimer();
        }

        private void CollectWheat()
        {
            int wheatAmount = PlayerBalance.Villagers.Sum(GetWheatProducing);
            PlayerBalance.WheatCount += wheatAmount;
            WorldData.HarvestingData.TotalWheatHarvested += wheatAmount;
            _soundService.PlayClip(SoundId.Harvesting);
        }

        private int GetWheatProducing(VillagerType villagerType) =>
            PlayerBalance.GetVillagersCount(villagerType) * _staticData.ForVillager(villagerType).WheatProducing;

        private void ResetTimer() =>
            TimeUntilHarvesting = _staticData.ForTimer(TimerId.Harvesting).Duration;
    }
}