using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;

namespace Dimasyechka.Code.ShopSystem
{
    public class RandomizableBoosterViewModel : MonoViewModel<RandomizableBooster>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<string> Title = new ReactiveProperty<string>();

        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> Icon = new ReactiveProperty<Sprite>();

        [RxAdaptableProperty]
        public ReactiveProperty<float> ObtainChance = new ReactiveProperty<float>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> Value = new ReactiveProperty<double>(); 


        protected override void OnSetupModel()
        {
            Title.Value = Model.Booster.Title;
            Icon.Value = Model.Booster.Icon;

            Value.Value = Model.Booster.Value;
            ObtainChance.Value = Model.ObtainChance;
        }
    }
}
