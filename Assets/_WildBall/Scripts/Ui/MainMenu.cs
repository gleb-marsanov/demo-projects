using System;
using Ui.Elements;
using UnityEngine;

namespace Ui
{
    public class MainMenu : MonoBehaviour
    {
        public Transform LevelSelectionRoot;
        public LevelSelectButton LevelButtonPrefab;

        public event Action<string> OnLevelSelected;

        private void Start()
        {
            foreach (LevelSelectButton button in LevelSelectionRoot.GetComponentsInChildren<LevelSelectButton>())
            {
                button.OnClick += (levelName) => OnLevelSelected?.Invoke(levelName);
            }
        }
    }
}