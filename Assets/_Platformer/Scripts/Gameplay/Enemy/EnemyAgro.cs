using System.Collections;
using Constants;
using Gameplay.Logic.Triggers;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class EnemyAgro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _trigger;
        [SerializeField] private Follow _follow;
        [SerializeField] private float _agroResetTime = 2f;

        [SerializeField] private bool _hasAggro;
        private Coroutine _aggroCoroutine;
        private WaitForSeconds _aggroResetCooldown;

        private void Start()
        {
            EnableFollow(false);
            _aggroResetCooldown = new WaitForSeconds(_agroResetTime);
        }

        private void OnEnable()
        {
            _trigger.TriggerEnter += TriggerEnter;
            _trigger.TriggerExit += TriggerExit;
        }

        private void OnDisable()
        {
            _trigger.TriggerEnter -= TriggerEnter;
            _trigger.TriggerExit -= TriggerExit;
        }

        private void TriggerEnter(Collider2D other)
        {
            if (!other.tag.Equals(Tags.Hero))
                return;
            
            StopAggroCoroutine();
            EnableFollow(true);
        }

        private void TriggerExit(Collider2D other)
        {
            if (!other.tag.Equals(Tags.Hero))
                return;

            if (!_hasAggro)
                return;

            _aggroCoroutine = StartCoroutine(DisableAggroAfterCooldown());
        }

        private void EnableFollow(bool value)
        {
            _hasAggro = value;
            _follow.enabled = value;
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine == null)
                return;

            StopCoroutine(_aggroCoroutine);
            _aggroCoroutine = null;
        }

        private IEnumerator DisableAggroAfterCooldown()
        {
            yield return _aggroResetCooldown;
            EnableFollow(false);
        }
    }
}