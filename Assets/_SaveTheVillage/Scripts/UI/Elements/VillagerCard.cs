using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Gameplay.Hiring;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.StaticData.Villagers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Elements
{
    public class VillagerCard : MonoBehaviour
    {
        [SerializeField] private Button _hireButton;
        [SerializeField] private Slider _hireProgressBar;
        [SerializeField] private TMP_Text _currentCount;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _label;

        private IPersistentProgressService _progressService;
        private IStaticDataService _staticData;
        private IHireService _hireService;
        private VillagerStaticData _villagerConfig;

        [Inject]
        public void Construct(IPersistentProgressService progressService, IStaticDataService staticData, IHireService hireService)
        {
            _hireService = hireService;
            _staticData = staticData;
            _progressService = progressService;
        }

        private PlayerProgress PlayerProgress => _progressService.Progress;
        private PlayerBalance PlayerBalance => _progressService.Progress.PlayerBalance;
        private WorldData WorldData => _progressService.Progress.WorldData;
        private VillagerStaticData CharacterConfig => _staticData.ForVillager(_villagerConfig.Type);

        public void Initialize(VillagerStaticData villager)
        {
            _villagerConfig = villager;
            
            _price.text = CharacterConfig.Price.ToString();
            _label.text = CharacterConfig.Name;
            
            UpdateView();
        }

        private void Start()
        {
            _hireButton.onClick.AddListener(Hire);
            PlayerBalance.OnChange += UpdateView;

        }

        private void OnDestroy()
        {
            PlayerBalance.OnChange -= UpdateView;
        }

        private void Update()
        {
            float timeUntilHire = WorldData.HireData.GetTimeUntilHiringFor(_villagerConfig.Type);

            UpdateHireProgress(timeUntilHire);
        }

        private void Hire()
        {
            _hireService.TryHire(_villagerConfig.Type);
        }

        private void UpdateView()
        {
            _currentCount.text = PlayerProgress.PlayerBalance.GetVillagersCount(_villagerConfig.Type).ToString();
            bool isAvailable = _hireService.CheckHireOpportunity(_villagerConfig.Type);
            _hireButton.interactable = isAvailable;
        }

        private void UpdateHireProgress(float timeUntilHire)
        {
            float hireDuration = CharacterConfig.HireDuration;
            float currentValue = hireDuration - timeUntilHire;
            _hireProgressBar.value = Mathf.Clamp01(currentValue / hireDuration);
        }
    }
}