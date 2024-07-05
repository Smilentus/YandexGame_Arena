using UnityEngine;

namespace Dimasyechka.Code.HealthSystem
{
    public class HealthComponentViewModelCoordinator : MonoBehaviour
    {
        [SerializeField]
        private HealthComponent _healthComponent;

        [SerializeField]
        private HealthComponentViewModel _viewModel;


        private void Awake()
        {
            if (_healthComponent == null || _viewModel == null) return;

            _viewModel.SetupModel(_healthComponent);
        }
    }
}
