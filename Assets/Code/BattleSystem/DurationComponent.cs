using System;
using UnityEngine;

namespace Dimasyechka
{
    public class DurationComponent : MonoBehaviour
    {
        public event Action onTimePassed;


        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _maxDuration;


        private bool _isEnabled;
        public bool IsEnabled => _isEnabled;


        private void FixedUpdate()
        {
            if (_isEnabled)
            {
                Processing();
            }
        }

        public void SetDuration(float duration)
        {
            _duration = duration;
            _maxDuration = duration;
        }

        public void StartTimer()
        {
            if (_isEnabled) return;

            _duration = _maxDuration;
            _isEnabled = true;
        }
        

        private void Processing()
        {
            if (_duration <= 0) return;

            _duration -= Time.fixedDeltaTime;

            if (_duration <= 0)
            {
                _isEnabled = false;
                _duration = _maxDuration;
                onTimePassed?.Invoke();
            }
        }
    }
}
