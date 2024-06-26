using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class PlayerBoosterInteractionWindow : BaseShopViewModel<PlayerBooster>
    {
        [SerializeField]
        private PlayerBoosterViewModel _playerBoosterViewModel;


        private PlayerBoostersContainer _container;

        [Inject]
        public void Construct(PlayerBoostersContainer container)
        {
            _container = container;
        }


        protected override void OnSetupModel()
        {
            _playerBoosterViewModel.SetupModel(Model);
        }


        [RxAdaptableMethod]
        public void Equip()
        {
            _container.TryEquipBooster(Model.Guid);
            Hide();
        }

        [RxAdaptableMethod]
        public void Remove()
        {
            _container.RemoveBooster(Model.Guid);
            Hide();
        }
    }
}
