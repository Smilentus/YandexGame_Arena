using Dimasyechka.Code.BoostingSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.ShopSystem
{
    public class BoosterShopMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private BoosterShopViewModel _boosterShopViewModel;

        [SerializeField]
        private PlayerBoosterInteractionWindow _playerBoosterInteractionWindow;


        public override void InstallBindings()
        {
            BindBoosterShopViewModel();
            BindPlayerBoosterInteractionWindow();
        }

        private void BindPlayerBoosterInteractionWindow()
        {
            Container.Bind<PlayerBoosterInteractionWindow>().FromInstance(_playerBoosterInteractionWindow).AsSingle();
        }

        private void BindBoosterShopViewModel()
        {
            Container.Bind<BoosterShopViewModel>().FromInstance(_boosterShopViewModel).AsSingle();
            Container.Bind<PlayerBoosterViewModelFactory>().FromNew().AsSingle();
        }
    }
}
