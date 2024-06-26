using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Code.Windows;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class PlayerAvailableBoostersContainerViewModel : BaseShopViewModel<PlayerBoostersContainer>
    {
        [SerializeField]
        private ObtainedPlayerBoosterViewModel _obtainedBoosterViewModelPrefab;

        [SerializeField]
        private Transform _contentParent;


        private PlayerBoosterViewModelFactory _factory;
        private PlayerBoostersWarehouse _boostersWarehouse;
        private PlayerBoosterInteractionWindow _interactionWindow;

        [Inject]
        public void Construct(
            PlayerBoostersContainer container, 
            PlayerBoosterViewModelFactory factory,
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
            Model.onAvailableBoostersChanged += OnAvailableBoostersChanged;
        }

        private void OnAvailableBoostersChanged()
        {
            DrawAvailableBoosters();
        }


        public override void OnDrawUI()
        {
            _interactionWindow.Hide();
            DrawAvailableBoosters();
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


    public class PlayerBoosterViewModelFactory : DiCreationFactory<ObtainedPlayerBoosterViewModel> { }
}