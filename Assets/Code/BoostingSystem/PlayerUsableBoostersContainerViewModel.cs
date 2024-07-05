using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Lubribrary.RxMV.Core;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class PlayerUsableBoostersContainerViewModel : MonoViewModel<PlayerBoostersContainer>
    {
        [SerializeField]
        private UsablePlayerBoosterViewModel _usableBoosterViewModelPrefab;

        [SerializeField]
        private Transform _contentParent;


        private PlayerUsableBoosterViewModelFactory _factory;
        private PlayerBoostersWarehouse _boostersWarehouse;
        private PlayerBoosterInteractionWindow _interactionWindow;

        [Inject]
        public void Construct(
            PlayerUsableBoosterViewModelFactory factory,
            PlayerBoostersWarehouse warehouse,
            PlayerBoosterInteractionWindow interactionWindow)
        {
            _factory = factory;
            _boostersWarehouse = warehouse;
            _interactionWindow = interactionWindow;
        }


        protected override void OnSetupModel()
        {
            Model.onUsedBoostersChanged += OnUsedBoostersChanged;
        }

        protected override void OnRemoveModel()
        {
            Model.onUsedBoostersChanged -= OnUsedBoostersChanged;
        }

        private void OnUsedBoostersChanged()
        {
            DrawUI();
        }


        public void DrawUI()
        {
            _interactionWindow.Hide();
            DrawUsedBoosters();
        }



        private void DrawUsedBoosters()
        {
            for (int i = _contentParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_contentParent.GetChild(i).gameObject);
            }

            for (int i = 0; i < Model.UsedBoosters.Length; i++)
            {
                UsablePlayerBoosterViewModel viewModel = _factory.InstantiateForComponent(_usableBoosterViewModelPrefab.gameObject, _contentParent);

                viewModel.SetSlotIndex(i);
                viewModel.SetupModel(Model.UsedBoosters[i]);
            }
        }
    }


    public class PlayerUsableBoosterViewModelFactory : DiCreationFactory<UsablePlayerBoosterViewModel> { }
}