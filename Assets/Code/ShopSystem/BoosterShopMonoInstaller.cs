using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.ShopSystem
{
    public class BoosterShopMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private BoosterShopViewModel _boosterShopViewModel;


        public override void InstallBindings()
        {
            BindBoosterShopViewModel();
        }

        private void BindBoosterShopViewModel()
        {
            Container.Bind<BoosterShopViewModel>().FromInstance(_boosterShopViewModel).AsSingle();
        }
    }
}
