using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Elements
{
    public class LevelSelectButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private Button _button;

        public event Action<string> OnClick;

        private string LevelName { get; set; }

        public void Initialize(string levelName)
        {
            LevelName = levelName;
            _label.text = LevelName;
        }

        private void Start()
        {
            _button.onClick.AddListener(() => OnClick?.Invoke(LevelName));
        }
    }
}