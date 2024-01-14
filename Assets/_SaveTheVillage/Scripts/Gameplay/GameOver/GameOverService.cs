using _SaveTheVillage.Scripts.Gameplay.Sounds;
using _SaveTheVillage.Scripts.StaticData.Sounds;
using _SaveTheVillage.Scripts.StaticData.Windows;
using _SaveTheVillage.Scripts.UI.Services.Windows;

namespace _SaveTheVillage.Scripts.Gameplay.GameOver
{
    internal class GameOverService : IGameOverService
    {
        private readonly IWindowService _windowService;
        private readonly ISoundService _soundService;

        public GameOverService(IWindowService windowService, ISoundService soundService)
        {
            _windowService = windowService;
            _soundService = soundService;
        }
        
        public GameOverReason GameOverReason { get; private set; }

        public void FinishGame(GameOverReason reason)
        {
            GameOverReason = reason;
            _windowService.Open(WindowId.GameOver);
            _soundService.PlayClip(SoundId.GameOver);
        }
    }
}