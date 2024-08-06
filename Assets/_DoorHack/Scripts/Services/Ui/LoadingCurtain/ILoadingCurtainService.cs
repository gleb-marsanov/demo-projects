namespace Services.Ui.LoadingCurtain
{
    public interface ILoadingCurtainService : IService
    {

        void Show();
        void Hide();
        void SetProgress(float value, string description = "");
    }

}