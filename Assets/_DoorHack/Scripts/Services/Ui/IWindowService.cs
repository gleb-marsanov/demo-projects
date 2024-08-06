namespace Services.Ui
{
    public interface IWindowService : IService
    {
        void ShowGameHud();
        void ShowGameOverWindow();
        void ShowVictoryWindow();
    }
}