using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimasyechka
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
