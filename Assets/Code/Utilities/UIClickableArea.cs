using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Dimasyechka
{
    public class UIClickableArea : MonoBehaviour, IPointerClickHandler
    {
        public event Action onClicked;


        [SerializeField]
        private float _clickDelay = 0.125f;

        private float _runtimeClickDelay = 0;


        private void OnEnable()
        {
            _runtimeClickDelay = 0;
        }


        private void FixedUpdate()
        {
            if (!isActiveAndEnabled) return;

            if (_runtimeClickDelay > 0)
            {
                _runtimeClickDelay -= Time.fixedDeltaTime;
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (_runtimeClickDelay > 0) return;

            _runtimeClickDelay = _clickDelay;

            onClicked?.Invoke();
        }
    }
}
