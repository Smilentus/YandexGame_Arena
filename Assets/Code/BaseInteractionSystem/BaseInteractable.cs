using UnityEngine;

namespace Dimasyechka.Code.BaseInteractionSystem
{
    public class BaseInteractable : MonoBehaviour
    {
        [field: SerializeField]
        public string CustomInteractionTitle { get; set; } = "Взаимодействовать";


        [field: SerializeField]
        public bool IsInteractable { get; set; } = true;


        private bool _isInteractionInProgress = false;


        public void StartInteraction()
        {
            if (_isInteractionInProgress) return;

            _isInteractionInProgress = true;

            Debug.Log("Interaction Started");

            OnInteractionStarted();
        }

        public virtual void OnInteractionStarted() { }

        public void EndInteraction()
        {
            if (!_isInteractionInProgress) return;

            _isInteractionInProgress = false;

            Debug.Log("Interaction Ended");

            OnInteractionEnded();
        }

        public virtual void OnInteractionEnded() { }
    }
}
