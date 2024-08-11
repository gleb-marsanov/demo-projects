using Infrastructure;
using Infrastructure.Services.Score;
using UnityEngine;
using Zenject;

namespace Gameplay.InteractiveObjects
{
    public class Pickup : MonoBehaviour
    {
        private IScoreService _score;

        [Inject]
        public void Construct(IScoreService score)
        {
            _score = score;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.HeroTag))
                return;

            _score.AddPoints(1);
            Destroy(gameObject);
        }
    }
}