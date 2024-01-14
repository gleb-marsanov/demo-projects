using _SaveTheVillage.Scripts.Data.Quests;
using _SaveTheVillage.Scripts.StaticData.Quests;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _SaveTheVillage.Scripts.UI.Elements
{
    public class QuestCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Slider _progressor;
        [SerializeField] private TMP_Text _progressText;

        private QuestStaticData _questConfig;
        private QuestProgress _questProgress;

        public void Initialize(QuestStaticData questConfig, QuestProgress questProgress)
        {
            _questConfig = questConfig;
            _questProgress = questProgress;
            _label.text = _questConfig.name;
            _description.text = string.Format(_questConfig.Description, _questConfig.TargetAmount);
            UpdateProgressor();
        }

        private void Start()
        {
            _questProgress.OnChange += UpdateProgressor;
        }

        private void OnDestroy()
        {
            _questProgress.OnChange -= UpdateProgressor;
        }

        private void UpdateProgressor()
        {
            float progress = (float)_questProgress.CurrentProgress / _questConfig.TargetAmount;
            _progressor.value = Mathf.Clamp01(progress);
            _progressText.text = $"{_questProgress.CurrentProgress}/{_questConfig.TargetAmount}";
        }
    }
}