using Dimasyechka.Code.BoostingSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.ShopSystem
{
    public class BoosterShopMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private BoosterWindowViewModel _boosterWindowViewModel;

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
            Container.Bind<BoosterWindowViewModel>().FromInstance(_boosterWindowViewModel).AsSingle();
            Container.Bind<ObtainedPlayerBoosterViewModelFactory>().FromNew().AsSingle();
            Container.Bind<PlayerUsableBoosterViewModelFactory>().FromNew().AsSingle();
        }
    }
}
