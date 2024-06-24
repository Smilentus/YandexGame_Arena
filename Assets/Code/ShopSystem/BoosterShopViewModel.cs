using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.ShopSystem
{
    public class BoosterShopViewModel : BaseShopViewModel<BoosterShopController>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<double> BuyBoosterPrice = new ReactiveProperty<double>();


        [SerializeField]
        private RandomizableBoosterViewModel _boosterViewModel;

        [SerializeField]
        private Transform _contentParent;


        private RandomizableBoosterViewModelFactory _factory;


        [Inject]
        public void Construct(RandomizableBoosterViewModelFactory factory)
        {
            _factory = factory;
        }


        public void SetShop(BoosterShopController shopController)
        {
            SetupModel(shopController);

            DrawShopUI();

            BuyBoosterPrice.Value = Model.BuyPrice;

            this.Show();
        }


        [ContextMenu("Draw Shop UI")]
        public void DrawShopUI()
        {
            for (int i = _contentParent.childCount - 1; i >= 0; i--)
            {
                Destroy(_contentParent.GetChild(i).gameObject);
            }

            foreach (RandomizableBooster booster in Model.SellableBoosters)
            {
                RandomizableBoosterViewModel viewModel = _factory.Instantiate(_boosterViewModel, _contentParent);

                viewModel.SetupModel(booster);
            }
        }
    }


    public class RandomizableBoosterViewModelFactory
    {
        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer container)
        {
            _diContainer = container;
        }


        public RandomizableBoosterViewModel Instantiate(RandomizableBoosterViewModel viewModel, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<RandomizableBoosterViewModel>(viewModel, parent);
        }
    }


    public class DiCreationFactory<T>
    {
        private DiContainer _diContainer;

        [Inject]
        public void Construct(DiContainer container)
        {
            _diContainer = container;
        }

        public virtual T InstantiateForComponent(GameObject prefab, Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<T>(prefab, parent);
        }
    }
}
