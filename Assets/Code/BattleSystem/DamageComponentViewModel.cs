using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Code.WorldFloating;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.Extensions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public class DamageComponentViewModel : MonoViewModel<DamageComponent>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<double> Damage = new ReactiveProperty<double>();


        [SerializeField]
        private WorldFloatingObject _worldFloatingObjectPrefab;

        [SerializeField]
        private Transform _contentParent;


        private WorldFloatingObjectFactory _factory;

        [Inject]
        public void Construct(WorldFloatingObjectFactory factory)
        {
            _factory = factory;
        }


        protected override void OnSetupModel()
        {
            _disposablesStorage.AddToDisposables(Model.Damage.SubscribeToEachOther(Damage));

            Model.onStrikeMiss += OnStrikeMiss;
        }

        protected override void OnRemoveModel()
        {
            Model.onStrikeMiss -= OnStrikeMiss;
        }


        private void OnStrikeMiss()
        {
            if (_worldFloatingObjectPrefab == null || _contentParent == null) return;

            _factory.InstantiateForComponent(_worldFloatingObjectPrefab.gameObject, _contentParent);
        }
    }


    public class WorldFloatingObjectFactory : DiCreationFactory<WorldFloatingObject> { }
}
