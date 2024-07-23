using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dimasyechka.Code.BaseInteractionSystem
{
    public class TriggerInteractor : MonoBehaviour
    {
        public event Action<BaseInteractable> onInteractionStarted;
        public event Action<BaseInteractable> onInterationEnded;


        public event Action<BaseInteractable> onTriggerEntered;
        public event Action<BaseInteractable> onTriggerExited;


        [SerializeField]
        private LayerMask _interactableLayers;


        [SerializeField]
        private InputActionReference _interactionKey;

        private BaseInteractable _collidedBaseInteractable;


        private void OnEnable()
        {
            _interactionKey.action.Enable();
            _interactionKey.action.performed += OnInteractionPerformed;
        }

        private void OnDisable()
        {
            _interactionKey.action.Disable();
            _interactionKey.action.performed -= OnInteractionPerformed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_interactableLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                if (other.gameObject.TryGetComponent<BaseInteractable>(out BaseInteractable interactableObject))
                {
                    if (_collidedBaseInteractable != null)
                    {
                        _collidedBaseInteractable.EndInteraction();
                        onInterationEnded?.Invoke(_collidedBaseInteractable);
                    }

                    _collidedBaseInteractable = interactableObject;

                    if (!interactableObject.IsInteractable) return;

                    onTriggerEntered?.Invoke(_collidedBaseInteractable);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((_interactableLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                if (_collidedBaseInteractable != null)
                {
                    _collidedBaseInteractable.EndInteraction();
                    onInterationEnded?.Invoke(_collidedBaseInteractable);

                    onTriggerExited?.Invoke(_collidedBaseInteractable);

                    _collidedBaseInteractable = null;
                }
            }
        }


        private void OnInteractionPerformed(InputAction.CallbackContext ctx)
        {
            if (_collidedBaseInteractable != null)
            {
                if (_collidedBaseInteractable.IsInteractable)
                {
                    _collidedBaseInteractable.StartInteraction();
                    onInteractionStarted?.Invoke(_collidedBaseInteractable);
                }
            }
        }
    }
}
