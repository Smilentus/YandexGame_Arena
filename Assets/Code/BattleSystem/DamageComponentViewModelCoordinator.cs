using UnityEngine;

namespace Dimasyechka.Code.BattleSystem
{
    public class DamageComponentViewModelCoordinator : MonoBehaviour
    {
        [SerializeField]
        private DamageComponent _damage;

        [SerializeField]
        private DamageComponentViewModel _viewModel;


        private void Awake()
        {
            if (_viewModel == null || _damage == null) return;

            _viewModel.SetupModel(_damage);
        }
    }
}
