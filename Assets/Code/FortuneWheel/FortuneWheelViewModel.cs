using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Zenject;

namespace Dimasyechka
{
    public class FortuneWheelViewModel : BaseShopViewModel<FortuneWheel>
    {
        [Inject]
        public void Construct(FortuneWheel fortuneWheel)
        {
            ZenjectModel(fortuneWheel);
        }


        [RxAdaptableMethod]
        public void Spin()
        {
            Model.Spin();
        }
    }
}
