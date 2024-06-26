using Dimasyechka.Code.BoostingSystem;
using Dimasyechka.Code.PlayerControlSystems.TPSAssets;
using Dimasyechka.Code.Utilities;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.PlayerSystem
{
    public class PlayerMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private RuntimePlayerObject _runtimePlayerObject;

        [SerializeField]
        private ThirdPersonController _thirdPersonController;

        [SerializeField]
        private PlayerInputHandler _playerInputHandler;

        [SerializeField]
        private PlayerBlocker _playerBlocker;

        [SerializeField]
        private PlayerAvailableBoostersContainerViewModel _viewModel;


        public override void InstallBindings()
        {
            BindRuntimePlayerObject();
            BindPlayerInputHandler();
            BindThirdPersonController();
            BindPlayerBlocker();
            BindPlayerBoostersContainer();
            BindPlayerBoostersContainerViewModel();
        }

        private void BindPlayerBoostersContainerViewModel()
        {
            Container.Bind<PlayerAvailableBoostersContainerViewModel>().FromInstance(_viewModel).AsSingle();
        }

        private void BindPlayerBoostersContainer()
        {
            Container.Bind<PlayerBoostersContainer>().FromNew().AsSingle();
        }

        private void BindPlayerBlocker()
        {
            Container.Bind<PlayerBlocker>().FromInstance(_playerBlocker).AsSingle();
        }

        private void BindThirdPersonController()
        {
            Container.Bind<ThirdPersonController>().FromInstance(_thirdPersonController).AsSingle();
        }

        private void BindPlayerInputHandler()
        {
            Container.Bind<PlayerInputHandler>().FromInstance(_playerInputHandler).AsSingle();
        }

        private void BindRuntimePlayerObject()
        {
            Container.Bind<RuntimePlayerObject>().FromInstance(_runtimePlayerObject).AsSingle();
        }
    }
}
