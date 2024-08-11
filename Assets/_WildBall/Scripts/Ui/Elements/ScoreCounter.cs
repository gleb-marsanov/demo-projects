using Infrastructure.Services.Score;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Ui.Elements
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private IScoreService _score;

        [Inject]
        public void Construct(IScoreService score)
        {
            _score = score;
            score.Score.Subscribe(UpdateScore);
        }
        
        private void UpdateScore(int score)
        {
            _text.text = score.ToString();
        }
    }
}