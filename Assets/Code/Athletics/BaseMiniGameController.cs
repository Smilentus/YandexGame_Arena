using System;
using UnityEngine;

namespace Dimasyechka.Code.Athletics
{
    public abstract class BaseMiniGameController : MonoBehaviour
    {
        public event Action onMiniGameStarted;
        public event Action onMiniGameEnded;


        protected bool _isMiniGameRunning;


        public void StartMiniGame()
        {
            _isMiniGameRunning = true;

            OnMiniGameStarted();

            onMiniGameStarted?.Invoke();
        }

        protected abstract void OnMiniGameStarted();


        public void EndMiniGame()
        {
            _isMiniGameRunning = false;

            OnMiniGameEnded();

            onMiniGameEnded?.Invoke();
        }

        protected abstract void OnMiniGameEnded();
    }
}
