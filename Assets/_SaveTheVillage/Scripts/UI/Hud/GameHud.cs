using _SaveTheVillage.Scripts.Infrastructure.Time;
using _SaveTheVillage.Scripts.StaticData.Windows;
using _SaveTheVillage.Scripts.UI.Services.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Hud
{
    public class GameHud : MonoBehaviour
    {
        [SerializeField] private Transform _cardsContainer;
        [SerializeField] private Transform _questContainer;
        [SerializeField] private Button _pauseButton;

        private ITimeService _time;
        private IWindowService _windowService;

        [Inject]
        public void Construct(ITimeService timeService, IWindowService windowService)
        {
            _windowService = windowService;
            _time = timeService;
        }

        public Transform CardsContainer => _cardsContainer;
        public Transform QuestContainer => _questContainer;

        private void Start()
        {
            _pauseButton.onClick.AddListener(PauseGame);
        }

        private void PauseGame()
        {
            _windowService.Open(WindowId.Pause);
        }
    }
}