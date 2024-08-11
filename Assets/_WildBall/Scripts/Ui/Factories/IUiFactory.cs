namespace Ui.Factories
{
    public interface IUiFactory
    {
        MainMenu CreateMainMenu();
        void Cleanup();
    }
}