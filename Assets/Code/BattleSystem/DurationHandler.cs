using System;
using UnityEngine;

namespace Dimasyechka.Code.BattleSystem
{
    public class DurationHandler
    {
        public event Action onTimePassed;


        private float _duration;
        private float _maxDuration;


        private bool _isEnabled;
        public bool IsEnabled => _isEnabled;


        public void Update(float deltaTime)
        {
            if (_isEnabled)
            {
                Processing(deltaTime);
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
        

        private void Processing(float deltaTime)
        {
            if (_duration <= 0) return;

            _duration -= deltaTime;

            if (_duration <= 0)
            {
                onTimePassed?.Invoke();
                _isEnabled = false;
                _duration = _maxDuration;
            }
        }
    }
}
