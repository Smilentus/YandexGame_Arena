using Dimasyechka.Code.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dimasyechka.Code.PlayerControlSystems.TPSAssets
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private const string CursorHandler = "PlayerInputHandler";


        [Header("Actions")]
        [SerializeField]
        private InputActionReference _movementAction;

        [SerializeField]
        private InputActionReference _lookAction;

        [SerializeField]
        private InputActionReference _pressForLookAction;


        [Header("Character Input Values")]
        public Vector2 MovementVector;
        public Vector2 LookVector;
        public bool IsJumping;
        public bool IsSprinting;
        public bool IsLooking;

        [Header("Movement Settings")]
        public bool IsMovingAnalog;

        [Header("Mouse Cursor Settings")]
        public bool UseInputLook = true;

        public bool HideCursorOnLook = false;


        private bool _isInputBlocked = false;


        private void OnEnable()
        {
            _movementAction.action.Enable();

            _lookAction.action.Enable();
            _pressForLookAction.action.Enable();
        }


        public void SetInputBlockedState(bool isBlocked)
        {
            _isInputBlocked = isBlocked;
        }


        private void Update()
        {
            if (_isInputBlocked)
            {
                MovementVector = Vector2.zero;
                IsLooking = false;
                return;
            }

            MovementVector = _movementAction.action.ReadValue<Vector2>();

            if (_pressForLookAction.action.WasPressedThisFrame())
            {
                IsLooking = true;
            }

            if (_pressForLookAction.action.WasReleasedThisFrame())
            {
                IsLooking = false;
            }

            if (HideCursorOnLook)
            {
                if (IsLooking)
                {
                    CursorController.Instance.UnRegisterCursorController(CursorHandler);
                }
                else
                {
                    CursorController.Instance.RegisterCursorController(CursorHandler);
                }
            }

            LookVector = IsLooking ? _lookAction.action.ReadValue<Vector2>() : Vector2.zero;
        }
    }
}
