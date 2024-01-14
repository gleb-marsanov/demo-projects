using _SaveTheVillage.Scripts.Infrastructure.StaticData;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using UnityEngine;

namespace _SaveTheVillage.Scripts.Gameplay.Sounds
{
    internal class SoundService : ISoundService
    {
        private Transform _root;
        private AudioSource _audioSource;
        private AudioSource _backgroundMusic;

        private readonly IStaticDataService _staticData;

        public SoundService(IStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public bool IsMuted { get; private set; }

        public void MuteAudio()
        {
            IsMuted = true;
            _backgroundMusic.Pause();
        }

        public void UnmuteAudio()
        {
            IsMuted = false;
            _backgroundMusic.Play();
        }

        public void PlayClip(SoundId soundId)
        {
            if (IsMuted)
                return;
            
            if (_root == null)
                _root = new GameObject("Sounds").transform;

            SoundConfig soundConfig = _staticData.ForSound(soundId);
            var audioSource = new GameObject(soundId.ToString()).AddComponent<AudioSource>();
            audioSource.transform.SetParent(_root);
            audioSource.clip = soundConfig.Clip;
            audioSource.loop = soundConfig.Loop;
            audioSource.Play();
            audioSource.volume = soundConfig.Volume;

            if (!soundConfig.Loop)
            {
                Object.Destroy(audioSource.gameObject, soundConfig.Clip.length);
            }

            if (soundId == SoundId.BackgroundMusic)
            {
                if (_backgroundMusic != null)
                {
                    _backgroundMusic.Play();
                }
                else
                {
                    _backgroundMusic = audioSource;
                    audioSource.transform.SetParent(null);
                    Object.DontDestroyOnLoad(audioSource);
                }
            }
        }

    }
}