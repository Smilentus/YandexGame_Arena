using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using System;
using Dimasyechka.Code.Durability;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dimasyechka.Code.Mine
{
    public class MineableItemViewModel : MonoViewModel<MineableItem>, IPointerClickHandler
    {
        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> Icon = new ReactiveProperty<Sprite>();

        private Action<MineableItemViewModel> _callback;

        [SerializeField]
        private BlockDurabilityVisuals _blockDurabilityVisuals;

        public void SetCallback(Action<MineableItemViewModel> callback)
        {
            _callback = callback;
        }

        protected override void OnSetupModel()
        {
            _blockDurabilityVisuals.Init(Model.Durability);

            Icon.Value = Model.MineableIcon;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            _blockDurabilityVisuals.DecreaseDurability(1);

            if (_blockDurabilityVisuals.Durability <= 0)
            {
                _callback?.Invoke(this);
            }
        }
    }
}