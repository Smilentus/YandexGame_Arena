using Dimasyechka.Code.BoostingSystem;
using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dimasyechka.Code.ShopSystem
{
    public class BoosterShopViewModel : BaseShopViewModel<BoosterShopController>
    {
        [SerializeField]
        private PlayerBoosterViewModel _rewardViewModel;

        [SerializeField]
        private RectTransform _priceLayout;


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


        public override void OnShow()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_priceLayout);
        }

        public override void OnHide()
        {
            StopAllCoroutines();
        }

        public void SetShop(BoosterShopController shopController)
        {
            _rewardViewModel.gameObject.SetActive(false);

            SetupModel(shopController);

            BuyBoosterPrice.Value = Model.BuyPrice;

            //LayoutRebuilder.ForceRebuildLayoutImmediate(_priceLayout);

            DrawShopUI();

            this.Show();
        }


        [RxAdaptableMethod]
        public void BuyBooster()
        {
            string obtainedBoosterGuid;

            if (Model.TryBuyRandomBooster(out obtainedBoosterGuid))
            {
                _rewardViewModel.gameObject.SetActive(true);
                _rewardViewModel.SetupModel(Model.GetBoosterByGuid(obtainedBoosterGuid));

                StartCoroutine(WaitForReward());
            }
            else
            {
                Debug.Log($"Not enough money to buy booster!");
            }
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


        private IEnumerator WaitForReward()
        {
            yield return new WaitForSecondsRealtime(2f);

            _rewardViewModel.gameObject.SetActive(false);
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
