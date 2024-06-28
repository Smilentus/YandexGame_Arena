using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using Zenject;

namespace Dimasyechka
{
    public class FortuneWheelTimerButtonViewModel : MonoViewModel<FortuneWheel>, IPointerClickHandler
    {
        [RxAdaptableProperty]
        public ReactiveProperty<string> DeltaTimeSpan = new ReactiveProperty<string>();

        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsSpinBlocked = new ReactiveProperty<bool>();


        private FortuneWheelViewModel _fortuneWheelViewModel;

        [Inject]
        public void Construct(
            FortuneWheel fortuneWheel,
            FortuneWheelViewModel fortuneWheelViewModel)
        {
            _fortuneWheelViewModel = fortuneWheelViewModel;

            ZenjectModel(fortuneWheel);
        }


        protected override void OnSetupModel()
        {
            _disposablesStorage.AddToDisposables(Model.TimeFromLastSpin.Subscribe(x =>
            {
                DeltaTimeSpan.Value = x.ToString(@"mm\:ss");
            }));

            _disposablesStorage.AddToDisposables(Model.IsSpinAvailable.Subscribe(x =>
            {
                IsSpinBlocked.Value = !x;
            }));
        }


        [RxAdaptableMethod]
        public void OpenFortuneWheel()
        {
            _fortuneWheelViewModel.Show();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsSpinBlocked.Value) return;

            OpenFortuneWheel();
        }
    }
}
