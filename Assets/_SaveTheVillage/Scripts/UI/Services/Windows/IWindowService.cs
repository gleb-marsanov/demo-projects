using _SaveTheVillage.Scripts.StaticData.Windows;

namespace _SaveTheVillage.Scripts.UI.Services.Windows
{
    public interface IWindowService
    {
        void Open(WindowId windowId);
        void ShowPopup();
    }
}