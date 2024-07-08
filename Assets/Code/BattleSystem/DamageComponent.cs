using System;
using Dimasyechka.Code.HealthSystem;
using UniRx;
using UnityEngine;

namespace Dimasyechka.Code.BattleSystem
{
    public class DamageComponent : MonoBehaviour
    {
        public event Action onStrike;
        public event Action onStrikeMiss;


        public ReactiveProperty<double> Damage = new ReactiveProperty<double>();


        [SerializeField]
        private float _attackRadius = 3f;

        [SerializeField]
        private string _damageTag;


        [SerializeField]
        private float _attackDelaySeconds = 0.25f;

        [SerializeField]
        private float _attackCooldownSeconds = 2f;


        private DurationHandler _attackCooldownHandler;

        private DurationHandler _attackDelayDurationHandler;


        private bool _isEnabled;


        private void Awake()
        {
            _attackCooldownHandler = new DurationHandler();
            _attackDelayDurationHandler = new DurationHandler();

            _attackCooldownHandler.SetDuration(_attackCooldownSeconds);
            _attackDelayDurationHandler.SetDuration(_attackDelaySeconds);

            _attackDelayDurationHandler.onTimePassed += OnAttackDelayDurationPassed;
        }

        private void OnDestroy()
        {
            _attackDelayDurationHandler.onTimePassed -= OnAttackDelayDurationPassed;
        }


        private void FixedUpdate()
        {
            _attackCooldownHandler.Update(Time.fixedDeltaTime);
            _attackDelayDurationHandler.Update(Time.fixedDeltaTime);
        }


        private void OnTriggerStay(Collider other)
        {
            if (!_isEnabled) return;

            if (!other.tag.Equals(_damageTag)) return;

            if (_attackCooldownHandler.IsEnabled) return;

            if (_attackDelayDurationHandler.IsEnabled) return;

            TryAttack();
        }

        private void TryAttack()
        {
            _attackDelayDurationHandler.StartTimer();
        }

        private void OnAttackDelayDurationPassed()
        {
            CollectDamageablesAround();
            _attackCooldownHandler.StartTimer();
        }


        private void CollectDamageablesAround()
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, _attackRadius);

            int damageablesAround = 0;

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.tag.Equals(_damageTag))
                    {
                        if (collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                        {
                            if (!damageable.IsAlive()) return;

                            damageablesAround++;

                            damageable.DamageInstance(Damage.Value);
                        }
                    }
                }

                onStrike?.Invoke();

                if (damageablesAround == 0)
                {
                    onStrikeMiss?.Invoke();
                }
            }
        }


        public void Setup(double damage, string? damageTag = null)
        {
            Damage.Value = damage;

            if (damageTag != null)
            {
                _damageTag = damageTag;
            }
        }


        public void ToggleComponent(bool value)
        {
            _isEnabled = value;
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, _attackRadius);
        }
    }
}
