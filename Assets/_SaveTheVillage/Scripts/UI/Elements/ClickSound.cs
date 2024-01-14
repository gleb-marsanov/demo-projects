using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using UnityEngine;
using Zenject;

namespace _SaveTheVillage.Scripts.UI.Elements
{
    public class ClickSound : MonoBehaviour
    {
        private ISoundService _soundService;

        [Inject]
        public void Construct(ISoundService soundService)
        {
            _soundService = soundService;
        }

        private void Start()
        {
            var button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(PlayClickSound);
        }

        private void PlayClickSound() => 
            _soundService.PlayClip(SoundId.Click);
    }
}