using Dimasyechka.Code.BoostingSystem;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using Zenject;

namespace Dimasyechka.Code.PlayerSystem
{
    public class MultiplicationFactorViewModel : MonoViewModel<PlayerBoostersContainer>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<double> MultiplicationFactor = new ReactiveProperty<double>();


        [Inject]
        public void Construct(PlayerBoostersContainer container)
        {
            ZenjectModel(container);
        }


        protected override void OnSetupModel()
        {
            Model.onUsedBoostersChanged += OnUsedBoostersChanged;
            OnUsedBoostersChanged();
        }

        protected override void OnRemoveModel()
        {
            Model.onUsedBoostersChanged += OnUsedBoostersChanged;
        }

        private void OnUsedBoostersChanged()
        {
            MultiplicationFactor.Value = Model.GetMultiplicationFactor();
        }
    }
}
