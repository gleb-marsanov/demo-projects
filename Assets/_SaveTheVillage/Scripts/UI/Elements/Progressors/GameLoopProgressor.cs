using _SaveTheVillage.Scripts.Data;
using _SaveTheVillage.Scripts.Infrastructure.PersistentProgress;
using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Elements.Progressors
{
    public abstract class GameLoopProgressor : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;
        
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            StaticData = staticDataService;
            _progressService = progressService;
        }

        protected PlayerProgress PlayerProgress => _progressService.Progress;
        protected IStaticDataService StaticData { get; private set; }
        protected abstract float MaxValue { get; }
        protected abstract float CurrentValue { get; }

        protected abstract void OnStart();
        
        protected void UpdateProgressor()
        {
            _slider.value = Mathf.Clamp01(CurrentValue / MaxValue);
            _text.text = $"{Mathf.CeilToInt(CurrentValue)}";
        }

        private void Start() => 
            OnStart();
    }
}