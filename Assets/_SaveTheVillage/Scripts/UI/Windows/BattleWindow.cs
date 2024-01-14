using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Gameplay.Battles;
using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.Infrastructure.Time;
using _SaveTheVillage.Scripts.StaticData.Battle;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using TMPro;
using UnityEngine;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Windows
{
    public class BattleWindow : WindowBase
    {
        [SerializeField] private TMP_Text _battleText;
        [SerializeField] private Animation _showAnimation;

        private ITimeService _timeService;
        private IBattleService _battleService;
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct
        (
            ITimeService timeService,
            IBattleService battleService,
            IPersistentProgressService progressService,
            IStaticDataService staticDataService
        )
        {
            _progressService = progressService;
            _battleService = battleService;
            _timeService = timeService;
        }

        private PlayerBalance PlayerBalance => _progressService.Progress.PlayerBalance;

        protected override void Initialize()
        {
            BattleConfig battle = _battleService.GetNextBattleConfig();
            int playerWarriors = PlayerBalance.GetVillagersCount(VillagerType.Warrior);
            _battleText.text = $"{playerWarriors} - {battle.EnemiesCount}";
            _showAnimation.Play();
            _timeService.Pause();
        }

        protected override void OnClose()
        {
            _battleService.StartBattle();
            _timeService.Unpause();
        }
    }
}