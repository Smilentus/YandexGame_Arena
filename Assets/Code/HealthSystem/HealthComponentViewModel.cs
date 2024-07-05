using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.Extensions;
using UniRx;

namespace Dimasyechka.Code.HealthSystem
{
    public class HealthComponentViewModel : MonoViewModel<HealthComponent>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<double> Health = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> MaxHealth = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<float> HealthRatio = new ReactiveProperty<float>();

        protected override void OnSetupModel()
        {
            _disposablesStorage.AddToDisposables(Model.Health.SubscribeToEachOther(Health));
            _disposablesStorage.AddToDisposables(Model.MaxHealth.SubscribeToEachOther(MaxHealth));
            _disposablesStorage.AddToDisposables(Model.HealthRatio.SubscribeToEachOther(HealthRatio));
        }
    }
}
