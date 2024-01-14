using _SaveTheVillage.Scripts.StaticData.Windows;
using _SaveTheVillage.Scripts.UI.Services.Factory;

namespace _SaveTheVillage.Scripts.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId) => 
            _uiFactory.CreateWindow(windowId);

        public void ShowPopup()
        {
            
        }
    }
}