namespace _SaveTheVillage.Scripts.Infrastructure.States.GameStates
{
    public class QuitGameState : IState
    {
        public void Enter()
        {
            CloseGameApplication();
        }

        private void CloseGameApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}