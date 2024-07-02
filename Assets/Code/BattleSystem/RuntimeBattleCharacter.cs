using Dimasyechka.Code.HealthSystem;
using UniRx;
using UnityEngine;

namespace Dimasyechka
{
    public class RuntimeBattleCharacter : MonoBehaviour
    {
        public ReactiveProperty<string> CharacterName = new ReactiveProperty<string>();


        [SerializeField]
        protected HealthComponent _health;

        [SerializeField]
        protected DamageComponent _damage;


        private bool _isDead;


        private void Awake()
        {
            _health.onHealthBelowZero += OnHealthBelowZero;
        }

        private void OnDestroy()
        {
            _health.onHealthBelowZero -= OnHealthBelowZero;
        }

        
        protected virtual void OnHealthBelowZero() 
        {
            if (_isDead) return;

            _isDead = true;
            _damage.ToggleComponent(false);
        }


        public void SetupCharacter(string name, double health, double damage)
        {
            CharacterName.Value = name;

            _health?.SetHealthAndMaxHealth(health);

            _damage?.Setup(damage);
            _damage.ToggleComponent(true);
        }
    }
}
