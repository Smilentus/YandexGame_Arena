using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.Extensions;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Dimasyechka
{
    public class DamageComponentViewModel : MonoViewModel<DamageComponent>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<double> Damage = new ReactiveProperty<double>();


        protected override void OnSetupModel()
        {
            _disposablesStorage.AddToDisposables(Model.Damage.SubscribeToEachOther(Damage));
        }
    }
}
