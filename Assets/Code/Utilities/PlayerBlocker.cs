using Dimasyechka.Code.PlayerControlSystems.TPSAssets;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.Utilities
{
    public class PlayerBlocker : MonoBehaviour
    {
        private ThirdPersonController _thirdPersonController;
        private PlayerInputHandler _playerInputHandler;

        [Inject]
        public void Construct(ThirdPersonController thirdPersonController, PlayerInputHandler playerInputHandler)
        {
            _thirdPersonController = thirdPersonController;
            _playerInputHandler = playerInputHandler;
        }


        public void BlockPlayer()
        {
            _thirdPersonController.SetControllerBlockState(true);
            _playerInputHandler.SetInputBlockedState(true);
        }

        public void UnBlockPlayer()
        {
            _thirdPersonController.SetControllerBlockState(false);
            _playerInputHandler.SetInputBlockedState(false);
        }
    }
}
