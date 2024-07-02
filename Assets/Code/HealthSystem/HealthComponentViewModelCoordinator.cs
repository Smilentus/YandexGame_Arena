using Dimasyechka.Code.HealthSystem;
using UnityEngine;

namespace Dimasyechka
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
