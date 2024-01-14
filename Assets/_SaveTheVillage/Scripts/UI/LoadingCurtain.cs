using UnityEngine;

namespace _SaveTheVillage.Scripts.UI
{
    public class LoadingCurtain : MonoBehaviour
    {
        public Canvas Canvas;
        // public DownloadBar DownloadBar;

        public void Show()
        {
            Canvas.enabled = true;
        }

        public void Hide()
        {
            Canvas.enabled = false;
            // DownloadBar.gameObject.SetActive(false);
        }
    }
}