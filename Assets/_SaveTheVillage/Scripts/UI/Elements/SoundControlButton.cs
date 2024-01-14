using _SaveTheVillage.Scripts.Gameplay.Sounds;
using UnityEngine;
using Zenject;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace _SaveTheVillage.Scripts.UI.Elements
{
    public class SoundControlButton : MonoBehaviour
    {
        [SerializeField] private Button _soundButton;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Sprite _soundOffSprite;
        [SerializeField] private Sprite _soundOnSprite;

        private ISoundService _soundService;

        [Inject]
        public void Construct(ISoundService soundService)
        {
            _soundService = soundService;
        }

        private void Start()
        {
            _soundButton.onClick.AddListener(OnSoundButtonClick);
            _buttonImage.sprite = _soundService.IsMuted
                ? _soundOffSprite
                : _soundOnSprite;
        }

        private void OnSoundButtonClick()
        {
            if (_soundService.IsMuted)
                UnmuteAudio();
            else
                MuteAudio();
        }

        private void MuteAudio()
        {
            _soundService.MuteAudio();
            _buttonImage.sprite = _soundOffSprite;
        }

        private void UnmuteAudio()
        {
            _soundService.UnmuteAudio();
            _buttonImage.sprite = _soundOnSprite;
        }

    }
}