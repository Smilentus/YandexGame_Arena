using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.Extensions;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem.Trading
{
    public class BoosterTradingWindow : BaseWindowViewModel<BoosterTradingSystem>
    {
        [SerializeField]
        private TradingView _view;

        [SerializeField]
        private Transform _content;


        private TradingViewFactory _factory;

        [Inject]
        public void Construct(BoosterTradingSystem tradingSystem, TradingViewFactory factory)
        {
            _factory = factory;

            ZenjectModel(tradingSystem);
        }


        public override void OnDrawUI()
        {
            _content.DestroyChildren();

            for (int i = 0; i < Model.BoosterTradingRules.TradingRules.Count; i++)
            {
                TradingView tradingView = _factory.InstantiateForComponent(_view.gameObject, _content);

                tradingView.SetupModel(Model.BoosterTradingRules.TradingRules[i]);
                tradingView.SetCallback(OnClickCallback);
            }
        }


        private void OnClickCallback()
        {
            OnDrawUI();
        }
    }


    public class TradingViewFactory : DiCreationFactory<TradingView> { }
}
