using Dimasyechka.Code.Mine;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;

namespace Dimasyechka
{
    public class MineableRewardViewModel : MonoViewModel<MineableItem>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> RewardIcon = new ReactiveProperty<Sprite>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> RewardValue = new ReactiveProperty<double>();


        protected override void OnSetupModel()
        {
            RewardIcon.Value = Model.Icon;
            RewardValue.Value = Model.RewardValue;
        }
    }
}
