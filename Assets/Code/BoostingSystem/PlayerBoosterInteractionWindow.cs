using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class PlayerBoosterInteractionWindow : BaseWindowViewModel<PlayerBooster>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsEquipInteraction = new ReactiveProperty<bool>();

        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsUnEquipInteraction = new ReactiveProperty<bool>();


        [SerializeField]
        private PlayerBoosterViewModel _playerBoosterViewModel;


        private PlayerBoostersContainer _container;

        private int _clickedSlotIndex;

        [Inject]
        public void Construct(PlayerBoostersContainer container)
        {
            _container = container;
        }


        protected override void OnSetupModel()
        {
            _playerBoosterViewModel.SetupModel(Model);
        }


        public void SetEquipInteractions()
        {
            IsEquipInteraction.Value = true;
            IsUnEquipInteraction.Value = false;
        }

        public void SetUnEquipInteractions(int clickedSlot)
        {
            _clickedSlotIndex = clickedSlot;

            IsEquipInteraction.Value = false;
            IsUnEquipInteraction.Value = true;
        }


        [RxAdaptableMethod]
        public void Equip()
        {
            _container.TryEquipBooster(Model.Guid);
            Hide();
        }

        [RxAdaptableMethod]
        public void UnEquip()
        {
            _container.UnEquipBooster(_clickedSlotIndex);
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
