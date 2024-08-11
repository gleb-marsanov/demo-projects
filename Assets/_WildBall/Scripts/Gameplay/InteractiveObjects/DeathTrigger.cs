using Gameplay.Hero;
using Infrastructure;
using UnityEngine;

namespace Gameplay.InteractiveObjects
{
    public class DeathTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.HeroTag))
                return;

            var heroDeath = other.GetComponent<HeroDeath>();
            heroDeath.Die();
        }
    }

}