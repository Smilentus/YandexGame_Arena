using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;

namespace Dimasyechka.Code.BaseInteractionSystem
{
    public class TriggerInteractionView : MonoViewModel<TriggerInteractor>
    {
        [SerializeField]
        private TriggerInteractor _triggerInteractor;


        [RxAdaptableProperty]
        public ReactiveProperty<string> InteractableTitle = new ReactiveProperty<string>();

        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsViewEnabled = new ReactiveProperty<bool>();


        private void Awake()
        {
            if (_triggerInteractor != null)
            {
                SetupModel(_triggerInteractor);
            }
        }


        private void OnEnable()
        {
            Model.onTriggerEntered += OnTriggerEntered;
            Model.onTriggerExited += OnTriggerEnded;
            
            Model.onInteractionStarted += OnInteractionStarted;
        }

        private void OnDisable()
        {
            Model.onTriggerEntered -= OnTriggerEntered;
            Model.onTriggerExited -= OnTriggerEnded;

            Model.onInteractionStarted -= OnInteractionStarted;
        }


        private void OnTriggerEntered(BaseInteractable baseInteractable)
        {
            IsViewEnabled.Value = true;

            InteractableTitle.Value = baseInteractable.CustomInteractionTitle == "" ? "Взаимодействовать" : baseInteractable.CustomInteractionTitle;
        }

        private void OnTriggerEnded(BaseInteractable baseInteractable)
        {
            IsViewEnabled.Value = false;
        }


        private void OnInteractionStarted(BaseInteractable baseInteractable)
        {
            IsViewEnabled.Value = false;
        }
    }
}
