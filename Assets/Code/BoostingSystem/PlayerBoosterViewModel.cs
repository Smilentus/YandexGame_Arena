using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;

namespace Dimasyechka.Code.BoostingSystem
{
    public class PlayerBoosterViewModel : MonoViewModel<PlayerBooster>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<string> Title = new ReactiveProperty<string>();

        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> Icon = new ReactiveProperty<Sprite>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> Value = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsNotEmpty = new ReactiveProperty<bool>();

        [RxAdaptableProperty]
        public ReactiveProperty<Color> IconColor = new ReactiveProperty<Color>();


        private void Awake()
        {
            _allowNullModels = true;
        }


        protected override void OnSetupModel()
        {
            IsNotEmpty.Value = Model != null;

            if (Model == null)
            {
                IconColor.Value = new Color(0, 0, 0, 0);
                return;
            }

            IconColor.Value = new Color(1, 1, 1, 1);
            Title.Value = Model.Title;
            Icon.Value = Model.Icon;
            Value.Value = Model.Value;
        }

        protected override void OnRemoveModel()
        {
            Title.Value = $"";
            Icon.Value = null;
            Value.Value = 0f;
        }
    }
}
