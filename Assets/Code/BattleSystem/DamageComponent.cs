using Dimasyechka.Code.HealthSystem;
using UniRx;
using UnityEngine;

namespace Dimasyechka
{
    public class DamageComponent : MonoBehaviour
    {
        public ReactiveProperty<double> Damage = new ReactiveProperty<double>();


        [SerializeField]
        private string _damageTag;


        [SerializeField]
        private DurationComponent _duration;


        private bool _isEnabled;


        private void OnTriggerStay(Collider other)
        {
            if (!_isEnabled) return;

            if (!other.tag.Equals(_damageTag)) return;

            if (_duration.IsEnabled) return;

            if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                _duration.StartTimer();
                damageable.DamageInstance(Damage.Value);
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
    }
}
