using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameHud : MonoBehaviour
    {
        [field: SerializeField] public TimeDisplay TimeDisplay { get; private set; }
        [SerializeField] private TMP_Text _pin1, _pin2, _pin3;
        [SerializeField] private Button _tool1, _tool2, _tool3;
        public event Action<int> OnToolUsed;

        private void Start()
        {
            _tool1.onClick.AddListener(() => UseTool(0));
            _tool2.onClick.AddListener(() => UseTool(1));
            _tool3.onClick.AddListener(() => UseTool(2));
        }

        public void SetDoorPins(int pin1, int pin2, int pin3)
        {
            _pin1.text = pin1.ToString();
            _pin2.text = pin2.ToString();
            _pin3.text = pin3.ToString();
        }

        private void UseTool(int toolIndex) => 
            OnToolUsed?.Invoke(toolIndex);
    }
}