using _SaveTheVillage.Scripts.Infrastructure.Time;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Elements
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        
        private ITimeService _timeService;

        [Inject]
        public void Construct(ITimeService timeService)
        {
            _timeService = timeService;
        }

        private void Awake() => 
            _pauseButton.onClick.AddListener(OnPauseButtonClick);

        private void OnPauseButtonClick()
        {
            if (_timeService.IsPaused)
                _timeService.Unpause();
            else
                _timeService.Pause();
        }
    }
}