using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Gameplay.Battles;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.StaticData.Battle;
using TMPro;
using UnityEngine;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Elements
{
    public class BattleCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _upcommingBattleText;
        [SerializeField] private string _upcommingBattleTextFormat = "{0} Ninjas are on the way";

        private IPersistentProgressService _progressService;
        private IBattleService _battleService;

        [Inject]
        public void Construct
        (
            IPersistentProgressService progressService,
            IStaticDataService staticDataService,
            IBattleService battleService
        )
        {
            _progressService = progressService;
            _battleService = battleService;
        }

        private WorldData WorldData => _progressService.Progress.WorldData;

        private void Start()
        {
            WorldData.BattleData.OnChange += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            WorldData.BattleData.OnChange -= UpdateView;

        private void UpdateView()
        {
            BattleConfig battleConfig = _battleService.GetNextBattleConfig();
            _upcommingBattleText.text = string.Format(_upcommingBattleTextFormat, battleConfig.EnemiesCount);
        }
    }
}