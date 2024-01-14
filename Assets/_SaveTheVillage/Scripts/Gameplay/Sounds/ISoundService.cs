using _SaveTheVillage.Scripts.StaticData.Sounds;

namespace _SaveTheVillage.Scripts.Gameplay.Sounds
{
    public interface ISoundService
    {
        bool IsMuted { get; }
        void PlayClip(SoundId soundId);
        void MuteAudio();
        void UnmuteAudio();
    }
}