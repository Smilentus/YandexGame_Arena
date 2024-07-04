using System;
using UniRx;
using UnityEngine;

namespace Dimasyechka.Code.HealthSystem
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        public event Action onHealthBelowZero;

        public ReactiveProperty<double> Health = new ReactiveProperty<double>();
        public ReactiveProperty<double> MaxHealth = new ReactiveProperty<double>();
        public ReactiveProperty<float> HealthRatio = new ReactiveProperty<float>();


        private bool _isHealthBelowZero;
        

        public void SetHealthAndMaxHealth(double health)
        {
            Health.Value = health;
            MaxHealth.Value = health;
            HealthRatio.Value = (float)Health.Value / (float)MaxHealth.Value;
        }

        public void DamageInstance(double damage)
        {
            if (_isHealthBelowZero) return;

            Health.Value -= damage;

            HealthRatio.Value = Mathf.Clamp01((float)Health.Value / (float)MaxHealth.Value);

            if (Health.Value <= 0)
            {
                HealthRatio.Value = 0;
                Health.Value = 0;

                _isHealthBelowZero = true;
                
                onHealthBelowZero?.Invoke();
            }
        }
    }
}
