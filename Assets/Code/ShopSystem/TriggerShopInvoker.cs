using Dimasyechka.Code.BaseInteractionSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.ShopSystem
{
    public class TriggerShopInteractable : BaseInteractable
    {
        [SerializeField]
        private BoosterShopController _shopController;


        private BoosterShopViewModel _boosterShopViewModel;

        [Inject]
        public void Construct(BoosterShopViewModel boosterShopViewModel)
        {
            _boosterShopViewModel = boosterShopViewModel;
        }


        public override void OnInteractionStarted()
        {
            _boosterShopViewModel.SetShop(_shopController);
        }

        public override void OnInteractionEnded()
        {
            _boosterShopViewModel.Hide();
        }
    }
}
