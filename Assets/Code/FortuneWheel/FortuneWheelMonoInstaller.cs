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
            Container.Bind<FortuneWheelViewModel>().FromInstance(_fortuneWheelViewModel).AsSingle();
            Container.BindInterfacesAndSelfTo<FortuneWheel>().FromNew().AsSingle().NonLazy();
        }
    }
}
