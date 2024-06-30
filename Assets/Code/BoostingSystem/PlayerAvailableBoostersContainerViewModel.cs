using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Code.Windows;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class PlayerAvailableBoostersContainerViewModel : BaseShopViewModel<PlayerBoostersContainer>
    {
        [SerializeField]
        private PlayerUsableBoostersContainerViewModel _usableBoostersViewModel;

        [SerializeField]
        private ObtainedPlayerBoosterViewModel _obtainedBoosterViewModelPrefab;

        [SerializeField]
        private Transform _contentParent;


        private ObtainedPlayerBoosterViewModelFactory _factory;
        private PlayerBoostersWarehouse _boostersWarehouse;
        private PlayerBoosterInteractionWindow _interactionWindow;

        [Inject]
        public void Construct(
            PlayerBoostersContainer container, 
            ObtainedPlayerBoosterViewModelFactory factory,
            PlayerBoostersWarehouse warehouse,
            PlayerBoosterInteractionWindow interactionWindow)
        {
            _factory = factory;
            _boostersWarehouse = warehouse;
            _interactionWindow = interactionWindow;

            ZenjectModel(container);
        }


        protected override void OnSetupModel()
        {
            _usableBoostersViewModel.SetupModel(Model);
        }

        protected override void OnRemoveModel()
        {
            _usableBoostersViewModel.RemoveModel();

            Model.onAvailableBoostersChanged -= OnAvailableBoostersChanged;
        }

        private void OnAvailableBoostersChanged()
        {
            if (!isActiveAndEnabled) return;

            DrawAvailableBoosters();
        }


        public override void OnShow()
        {
            if (Model == null) return;

            Model.onAvailableBoostersChanged += OnAvailableBoostersChanged;
        }

        public override void OnHide()
        {
            if (Model == null) return;

            Model.onAvailableBoostersChanged -= OnAvailableBoostersChanged;
        }


        public override void OnDrawUI()
        {
            if (!isActiveAndEnabled) return;

            _interactionWindow.Hide();
            DrawAvailableBoosters();
            _usableBoostersViewModel.DrawUI();
        }



        private void DrawAvailableBoosters()
        {
            for (int i = _contentParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_contentParent.GetChild(i).gameObject);
            }

            List<PlayerBooster> obtainedSortedBoosters = new List<PlayerBooster>();

            foreach (var booster in Model.AvailableBoosters)
            {
                obtainedSortedBoosters.Add(_boostersWarehouse.GetBoosterByGuid(booster.Guid));
            }

            obtainedSortedBoosters = obtainedSortedBoosters.OrderByDescending(x => x.Value).ToList();

            foreach (var booster in obtainedSortedBoosters)
            {
                ObtainedPlayerBoosterViewModel viewModel = _factory.InstantiateForComponent(_obtainedBoosterViewModelPrefab.gameObject, _contentParent);

                viewModel.SetupModel(booster);
            }
        }
    }


    public class ObtainedPlayerBoosterViewModelFactory : DiCreationFactory<ObtainedPlayerBoosterViewModel> { }
}