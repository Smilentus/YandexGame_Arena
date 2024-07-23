using Dimasyechka.Code.BaseInteractionSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.ShopSystem
{
    public class TriggerShopInteractable : BaseInteractable
    {
        [SerializeField]
        private BoosterShopController _shopController;


        private BoosterWindowViewModel _boosterWindowViewModel;

        [Inject]
        public void Construct(BoosterWindowViewModel boosterWindowViewModel)
        {
            _boosterWindowViewModel = boosterWindowViewModel;
        }


        public override void OnInteractionStarted()
        {
            _boosterWindowViewModel.SetShop(_shopController);
        }

        public override void OnInteractionEnded()
        {
            _boosterWindowViewModel.Hide();
        }
    }
}
