using UI;

namespace Services.Ui.Factories
{
    public interface IUiFactory : IService
    {
        void CreateUiRoot();
        GameHud CreateGameHud();
        VictoryWindow CreateVictoryWindow();
        GameOverWindow CreateGameOverWindow();
        UI.LoadingCurtain CreateLoadingCurtain();
    }

}