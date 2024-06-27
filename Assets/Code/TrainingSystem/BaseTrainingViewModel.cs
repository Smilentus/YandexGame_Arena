using Dimasyechka.Code.Windows;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using System;
using UniRx;
using UnityEngine;

namespace Dimasyechka
{
    public class BaseTrainingViewModel : BaseShopViewModel<Transform>
    {       
        public event Action onTrainingCompleted;


        [RxAdaptableProperty]
        public ReactiveProperty<float> ClickedRatio = new ReactiveProperty<float>();

        [RxAdaptableProperty]
        public ReactiveProperty<Color> ProgressColor = new ReactiveProperty<Color>();


        [SerializeField]
        private UIClickableArea _clickableArea;


        [Header("Colors")]
        [SerializeField]
        private Color _startColor;

        [SerializeField]
        private Color _endColor;



        private bool _isAllowedToClick = true;


        private float _runtimeClickPower = 0;

        private float _clickPower = 0.15f;
        private float _upgradePower = 1f;

        private float _deceleratePower = 0.05f;
        private float _nulifyPower = 2f;

        
        public override void OnShow()
        {
            _clickableArea.onClicked += Click;
            _runtimeClickPower = 0;
            _isAllowedToClick = true;

            UpdateRatio();
            UpdateProgressColor();
        }

        public override void OnHide()
        {
            _clickableArea.onClicked -= Click;
        }


        private void FixedUpdate()
        {
            if (_runtimeClickPower > 0)
            {
                if (_isAllowedToClick)
                {
                    _runtimeClickPower -= Time.fixedDeltaTime * _deceleratePower;
                }
                else
                {
                    _runtimeClickPower -= Time.fixedDeltaTime * _nulifyPower;
                }
            }

            if (_runtimeClickPower <= 0)
            {
                _isAllowedToClick = true;
            }

            UpdateRatio();
            UpdateProgressColor();
        }


        private void UpdateRatio()
        {
            _runtimeClickPower = Mathf.Clamp01(_runtimeClickPower);
            ClickedRatio.Value = _runtimeClickPower;
        }


        public void Click()
        {
            if (!_isAllowedToClick) return;

            _runtimeClickPower += _clickPower;

            UpdateRatio();
            UpdateProgressColor();

            if (_runtimeClickPower >= 1)
            {
                _isAllowedToClick = false;

                onTrainingCompleted?.Invoke();
            }
        }

        private void UpdateProgressColor()
        {
            ProgressColor.Value = Color.Lerp(_startColor, _endColor, ClickedRatio.Value);
        }
    }
}
