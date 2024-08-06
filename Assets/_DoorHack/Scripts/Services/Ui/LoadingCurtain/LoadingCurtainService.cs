using Services.Ui.Factories;
using Object = UnityEngine.Object;

namespace Services.Ui.LoadingCurtain
{
    public class LoadingCurtainService : ILoadingCurtainService
    {
        private readonly UI.LoadingCurtain _loadingCurtain;

        public LoadingCurtainService(IUiFactory uiFactory)
        {
            _loadingCurtain = uiFactory.CreateLoadingCurtain();
            Object.DontDestroyOnLoad(_loadingCurtain.gameObject);
        }

        public void Show()
        {
            _loadingCurtain.Show();
            SetProgress(0f);
        }

        public void Hide() =>
            _loadingCurtain.Hide();

        public void SetProgress(float value, string description = "") =>
            _loadingCurtain.SetProgress(value, description);
    }
}