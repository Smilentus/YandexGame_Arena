using Dimasyechka.Code.BoostingSystem;
using Dimasyechka.Code.BoostingSystem.Trading;
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

        [SerializeField]
        private BoosterTradingWindow _boosterTradingWindow;



        public override void InstallBindings()
        {
            BindBoosterShopViewModel();
            BindPlayerBoosterInteractionWindow();
            BindTradingSystem();
        }

        private void BindTradingSystem()
        {
            Container.Bind<TradingViewFactory>().FromNew().AsSingle();
            Container.Bind<BoosterTradingSystem>().FromNew().AsSingle();
            Container.Bind<BoosterTradingWindow>().FromInstance(_boosterTradingWindow).AsSingle();
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
