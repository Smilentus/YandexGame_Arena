using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;

namespace Dimasyechka.Code.FortuneWheel
{
    public class PrizeViewModel : MonoViewModel<Prize>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<string> Title = new ReactiveProperty<string>();

        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> Icon = new ReactiveProperty<Sprite>();


        protected override void OnSetupModel()
        {
            Title.Value = Model.Title;
            Icon.Value = Model.Icon;
        }
    }
}
