using Dimasyechka.Code.ShopSystem;
using Zenject;

namespace Dimasyechka.Code.MonoInstallers
{
    public class FactoriesMonoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRandomizableBoosterViewModelFactory();
        }
        
        private void BindRandomizableBoosterViewModelFactory()
        {
            Container.Bind<RandomizableBoosterViewModelFactory>().FromNew().AsSingle();
        }
    }
}
