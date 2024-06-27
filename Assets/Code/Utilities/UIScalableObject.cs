using DG.Tweening;
using UnityEngine;

namespace Dimasyechka
{
    public class UIScalableObject : MonoBehaviour
    {
        [Header("Scale Settings")]
        [SerializeField]
        private float _scaleTime = 0.05f;

        [SerializeField]
        private float _scaleValue = 1;

        [SerializeField]
        private float _scaleMaxValue = 1.25f;

        [SerializeField]
        private float _scaleMinValue = 0.75f;

        [SerializeField]
        private float _scaleDirection = 1f;


        private void FixedUpdate()
        {
            CalculateAndSetScale();
        }


        private void CalculateAndSetScale()
        {
            if (_scaleValue >= _scaleMaxValue)
            {
                _scaleValue = _scaleMaxValue;
                _scaleDirection = -1f;
            }

            if (_scaleValue <= _scaleMinValue)
            {
                _scaleValue = _scaleMinValue;
                _scaleDirection = 1f;
            }

            _scaleValue += Time.fixedDeltaTime * _scaleTime * _scaleDirection;

            this.transform.DOScale(_scaleValue, Time.fixedDeltaTime);
        }
    }
}
