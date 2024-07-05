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
            _viewModel.SetupModel(_damage);
        }
    }
}
