using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.Extensions;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dimasyechka
{
    public class PlayerProgressMilestoneViewModel : MonoViewModel<PlayerProgressMilestone>, IPointerClickHandler
    {
        public event Action<PlayerProgressMilestone> onClaimed;


        [RxAdaptableProperty]
        public ReactiveProperty<string> PlayerPrefix = new ReactiveProperty<string>();
        
        [RxAdaptableProperty]
        public ReactiveProperty<double> Value = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> Icon = new ReactiveProperty<Sprite>();

        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsCompleted = new ReactiveProperty<bool>();

        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsOpened = new ReactiveProperty<bool>();


        protected override void OnSetupModel()
        {
            _disposablesStorage.AddToDisposables(Model.IsCompleted.SubscribeToEachOther(IsCompleted));
            _disposablesStorage.AddToDisposables(Model.IsOpened.SubscribeToEachOther(IsOpened));

            PlayerPrefix.Value = Model.PlayerPrefix;
            Value.Value = Model.Value;
            Icon.Value = Model.Achievement.Icon;
        }

        protected override void OnRemoveModel()
        {
            onClaimed = null;
        }


        [RxAdaptableMethod]
        public void ClaimMilestone()
        {
            if (!IsOpened.Value || IsCompleted.Value) return;

            onClaimed?.Invoke(Model);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ClaimMilestone();
        }
    }
}
