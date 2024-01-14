using System.Collections;
using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.Infrastructure;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Villagers;

namespace _SaveTheVillage.Scripts.Gameplay.Hiring
{
    internal class HireService : IHireService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly ITimeService _time;
        private readonly ISoundService _soundService;

        public HireService
        (
            ICoroutineRunner coroutineRunner,
            IStaticDataService staticData,
            IPersistentProgressService progressService,
            ITimeService time, ISoundService soundService)
        {
            _coroutineRunner = coroutineRunner;
            _staticData = staticData;
            _progressService = progressService;
            _time = time;
            _soundService = soundService;
        }

        private PlayerBalance PlayerBalance => _progressService.Progress.PlayerBalance;
        private HireData HireData => _progressService.Progress.WorldData.HireData;

        public bool TryHire(VillagerType type)
        {
            if (!CheckHireOpportunity(type))
                return false;

            _coroutineRunner.StartCoroutine(HireCharacter(type));
            PlayerBalance.WheatCount -= _staticData.ForVillager(type).Price;
            return true;
        }

        public bool CheckHireOpportunity(VillagerType type)
        {
            bool isHiring = _progressService.Progress.WorldData.HireData.GetTimeUntilHiringFor(type) > 0;
            bool enoughWheat = PlayerBalance.WheatCount >= _staticData.ForVillager(type).Price;
            return enoughWheat && !isHiring;
        }

        private IEnumerator HireCharacter(VillagerType type)
        {
            VillagerStaticData villager = _staticData.ForVillager(type);

            HireData.SetHireTime(villager.Type, villager.HireDuration);

            while (HireData.GetTimeUntilHiringFor(villager.Type) > 0)
            {
                float timeUntilHire = HireData.GetTimeUntilHiringFor(villager.Type) - _time.DeltaTime;
                HireData.SetHireTime(villager.Type, timeUntilHire);
                yield return null;
            }

            PlayerBalance.AddVillager(villager.Type);
            HireData.TotalVillagersHired++;
            _soundService.PlayClip(SoundId.VillagerHired);
        }
    }
}