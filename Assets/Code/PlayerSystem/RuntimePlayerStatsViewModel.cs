using Dimasyechka.Code.PlayerSystem;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using Zenject;

namespace Dimasyechka
{
    public class RuntimePlayerStatsViewModel : MonoViewModel<RuntimePlayerObject>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<double> TotalPower = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> BodyPower = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> HandsPower = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<double> LegsPower = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<ulong> Coins = new ReactiveProperty<ulong>();


        [Inject]
        public void Construct(RuntimePlayerObject playerObject)
        {
            ZenjectModel(playerObject);
        }


        private void Update()
        {
            UpdateViewModel();
        }


        protected override void OnSetupModel()
        {
            if (Model == null) return;

            Model.onPlayerStatsLoaded += UpdateViewModel;

            UpdateViewModel();
        }

        protected override void OnRemoveModel()
        {
            Model.onPlayerStatsLoaded -= UpdateViewModel;
        }


        private void UpdateViewModel()
        {
            if (Model == null) return;


            TotalPower.Value = Model.RuntimePlayerStats.TotalPower;

            BodyPower.Value = Model.RuntimePlayerStats.BodyPower;
            HandsPower.Value = Model.RuntimePlayerStats.HandsPower;
            LegsPower.Value = Model.RuntimePlayerStats.LegsPower;

            Coins.Value = Model.RuntimePlayerStats.Coins;
        }
    }
}
