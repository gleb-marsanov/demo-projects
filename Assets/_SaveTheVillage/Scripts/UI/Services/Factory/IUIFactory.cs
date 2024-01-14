using _SaveTheVillage.Scripts.StaticData.Windows;

namespace _SaveTheVillage.Scripts.UI.Services.Factory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        void CreateWindow(WindowId windowId);
        void CreateHud();
        void Cleanup();
    }
}