using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class FortuneWheelMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private FortuneWheelViewModel _fortuneWheelViewModel;


        public override void InstallBindings()
        {
            BindFortuneWheelViewModel();
            BindFortuneWheel();
            BindFortuneWheelPrizeGiver();
        }

        private void BindFortuneWheel()
        {
            Container.BindInterfacesAndSelfTo<FortuneWheel>().FromNew().AsSingle().NonLazy();
        }

        private void BindFortuneWheelPrizeGiver()
        {
            Container.BindInterfacesAndSelfTo<FortuneWheelPrizeGiver>().FromNew().AsSingle().NonLazy();
        }

        private void BindFortuneWheelViewModel()
        {
            Container.Bind<FortuneWheelViewModel>().FromInstance(_fortuneWheelViewModel).AsSingle();
        }
    }
}
