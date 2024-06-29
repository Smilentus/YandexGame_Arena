using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;

namespace Dimasyechka
{
    public class FortuneWheelSlotViewModel : MonoViewModel<FortuneWheelSlotData>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> Icon = new ReactiveProperty<Sprite>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> Value = new ReactiveProperty<double>();


        protected override void OnSetupModel()
        {
            Icon.Value = Model.Prize.Icon;
            Value.Value = Model.Value;
        }
    }
}
