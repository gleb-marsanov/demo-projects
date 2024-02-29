using Constants;
using Gameplay.Hero;
using UnityEngine;

namespace Gameplay.Logic.Triggers
{
    public class HeroDeathTrigger : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _trigger;

        private void OnEnable()
        {
            _trigger.TriggerEnter += TriggerEnter;
        }

        private void TriggerEnter(Collider2D obj)
        {
            if (obj.tag.Equals(Tags.Hero))
                obj.GetComponent<HeroDeath>().ResetHealthToZero();
        }
    }
}