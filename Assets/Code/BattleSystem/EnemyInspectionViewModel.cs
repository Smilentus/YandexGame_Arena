using System;
using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public sealed class EnemyInspectionViewModel : BaseWindowViewModel<BattleSettings>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<string> EnemyName = new ReactiveProperty<string>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> EnemyHealth = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> EnemyDamage = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<string> WinCoinsString = new ReactiveProperty<string>(); 


        [RxAdaptableProperty]
        public ReactiveProperty<int> EnemiesCounter = new ReactiveProperty<int>();

        [RxAdaptableProperty]
        public ReactiveProperty<int> MinSliderValue = new ReactiveProperty<int>();

        [RxAdaptableProperty]
        public ReactiveProperty<int> MaxSliderValue = new ReactiveProperty<int>();


        private int _minEnemies = 1;
        private int _maxEnemies = 10;


        private BattleController _battleController;

        [Inject]
        public void Construct(BattleController battleController)
        {
            _battleController = battleController;
        }


        protected override void OnSetupModel()
        {
            EnemiesCounter.Value = 1;

            EnemyName.Value = Model.EnemyProfile.CharacterName;
            EnemyDamage.Value = Model.EnemyProfile.Damage;
            EnemyHealth.Value = Model.EnemyProfile.Health;

            MinSliderValue.Value = _minEnemies;
            MaxSliderValue.Value = _maxEnemies;

            WinCoinsString.Value = EnemiesCounter.Value == 1 
                ? $"Награда: {Model.WinCoins}<sprite=0>" 
                : $"Награда: {Model.WinCoins}<sprite=0> x{EnemiesCounter.Value}";
        }


        [RxAdaptableMethod]
        public void OnSliderValueChanged(int value)
        {
            value = Mathf.Clamp(value, _minEnemies, _maxEnemies);
            Model.EnemiesInBattle = value;

            EnemiesCounter.Value = value;
            WinCoinsString.Value = value == 1 
                ? $"Награда: {Model.WinCoins}<sprite=0>" 
                : $"Награда: {Model.WinCoins}<sprite=0> x{value}";
        }


        [RxAdaptableMethod]
        public void PlusButton()
        {
            EnemiesCounter.Value = Math.Clamp(EnemiesCounter.Value + 1, _minEnemies, _maxEnemies);
        }

        [RxAdaptableMethod]
        public void MinusButton()
        {
            EnemiesCounter.Value = Math.Clamp(EnemiesCounter.Value - 1, _minEnemies, _maxEnemies);
        }


        [RxAdaptableMethod]
        public void StartBattle()
        {
            _battleController.StartBattle(Model);
            Hide();
        }
    }
}
