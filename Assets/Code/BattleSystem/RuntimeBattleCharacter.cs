using Dimasyechka.Code.HealthSystem;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.RxLink;
using System;
using UniRx;
using UnityEngine;

namespace Dimasyechka.Code.BattleSystem
{
    public class RuntimeBattleCharacter : MonoBehaviour, IRxLinkable
    {
        private readonly int _attackTrigger = Animator.StringToHash("AttackTrigger");


        public event Action onDead;

        [RxAdaptableProperty]
        public ReactiveProperty<string> CharacterName = new ReactiveProperty<string>();


        [SerializeField]
        protected HealthComponent _health;
        public HealthComponent Health => _health;

        [SerializeField]
        protected DamageComponent _damage;
        public DamageComponent Damage => _damage;


        [SerializeField]
        protected Animator _animator;
        public Animator AnimatorReference => _animator;


        [SerializeField]
        private bool _useDeadAnimation = true;


        [SerializeField]
        private ParticleSystem _deadParticles;


        private bool _isDead;
        private static readonly int IsDead = Animator.StringToHash("IsDead");


        private void Awake()
        {
            _health.onHealthBelowZero += OnHealthBelowZero;
            _damage.onStrike += OnStrikeCallback;
        }

        private void OnDestroy()
        {
            _health.onHealthBelowZero -= OnHealthBelowZero;
            _damage.onStrike -= OnStrikeCallback;
        }


        protected virtual void OnHealthBelowZero()
        {
            if (_isDead) return;

            _isDead = true;

            if (_animator != null && _useDeadAnimation)
            {
                _animator.SetBool(IsDead, true);
            }

            if (_deadParticles != null)
            {
                _deadParticles.Play();
            }

            _damage.ToggleComponent(false);

            onDead?.Invoke();
        }


        public void SetupCharacter(string name, double health, double damage)
        {
            CharacterName.Value = name;

            _health.SetHealthAndMaxHealth(health);

            _damage.Setup(damage);
            _damage.ToggleComponent(true);
        }


        public void SetupAnimator(Animator animator)
        {
            _animator = animator;
        }


        private void OnStrikeCallback()
        {
            if (_animator != null)
            {
                _animator.SetTrigger(_attackTrigger);
            }
        }
    }
}
