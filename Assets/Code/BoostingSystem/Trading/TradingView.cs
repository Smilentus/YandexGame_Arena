using System;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem.Trading
{
    public class TradingView : MonoViewModel<BoosterTradingRules>, IPointerClickHandler
    {
        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> InputBoosterSprite = new ReactiveProperty<Sprite>();

        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> OutputBoosterSprite = new ReactiveProperty<Sprite>();


        [RxAdaptableProperty]
        public ReactiveProperty<int> InputBoosterCount = new ReactiveProperty<int>();

        [RxAdaptableProperty]
        public ReactiveProperty<int> OutputBoosterCount = new ReactiveProperty<int>();


        [RxAdaptableProperty]
        public ReactiveProperty<Color> AvailableColor = new ReactiveProperty<Color>();

        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsAvailable = new ReactiveProperty<bool>();


        private Action _callback;


        private PlayerBoostersContainer _playerBoosters;

        [Inject]
        public void Construct(PlayerBoostersContainer playerBoosters)
        {
            _playerBoosters = playerBoosters;
        }


        public void SetCallback(Action callback)
        {
            _callback = callback;
        }


        protected override void OnSetupModel()
        {
            InputBoosterSprite.Value = Model.InputBooster.Icon;
            InputBoosterCount.Value = Model.InputAmount;

            OutputBoosterSprite.Value = Model.OutputBooster.Icon;
            OutputBoosterCount.Value = Model.OutputAmount;

            IsAvailable.Value = _playerBoosters.IsEnoughBoosters(Model.InputBooster.Guid, Model.InputAmount);
            AvailableColor.Value = IsAvailable.Value ? Color.white : Color.red;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsAvailable.Value) return;

            _playerBoosters.TradeBoosters(Model);

            IsAvailable.Value = _playerBoosters.IsEnoughBoosters(Model.InputBooster.Guid, Model.InputAmount);
            AvailableColor.Value = IsAvailable.Value ? Color.white : Color.red;

            if (_callback != null)
            {
                _callback?.Invoke();
            }
        }
    }
}
