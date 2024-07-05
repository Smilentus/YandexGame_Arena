using System;
using System.Collections;
using Dimasyechka.Code.Utilities;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.RxLink;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.ScreenFader
{
    public class GeneralScreenFader : MonoBehaviour, IRxLinkable
    {
        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsFading = new ReactiveProperty<bool>();

        [RxAdaptableProperty]
        public ReactiveProperty<string> FadeTitle = new ReactiveProperty<string>();

        [RxAdaptableProperty] 
        public ReactiveProperty<float> FadeAmount = new ReactiveProperty<float>();

        [RxAdaptableProperty]
        public ReactiveProperty<Color> FadeColor = new ReactiveProperty<Color>();


        private float _fadeSpeedSeconds = 0.5f;
        private float _fadeFullDelay = 0.5f;


        private Coroutine _fader;
        private PlayerBlocker _playerBlocker;

        [Inject]
        public void Construct(PlayerBlocker playerBlocker)
        {
            _playerBlocker = playerBlocker;
        }


        public void FadeInAndOut(FadeSettings fadeSettings)
        {
            if (_fader != null)
                StopCoroutine(_fader);

            _fader = StartCoroutine(FadeInAndOutCoroutine(fadeSettings));
        }


        private IEnumerator FadeInAndOutCoroutine(FadeSettings fadeSettings)
        {
            IsFading.Value = true;

            FadeTitle.Value = fadeSettings.FadeTitle;

            if (fadeSettings.BlockPlayerWithFade)
            {
                _playerBlocker.BlockPlayer();
            }

            FadeAmount.Value = 0;
            FadeColor.Value = new Color(0, 0, 0, 0);

            float delta = 1f / 100f / _fadeSpeedSeconds;

            while (FadeAmount.Value < 1)
            {
                FadeColor.Value += new Color(0, 0, 0, delta);
                FadeAmount.Value += delta;

                yield return new WaitForSecondsRealtime(0.01f);
            }

            FadeAmount.Value = 1f;
            FadeColor.Value = new Color(0, 0, 0, 1);

            if (fadeSettings.OnFadeInCallback != null)
            {
                fadeSettings.OnFadeInCallback?.Invoke();
            }

            yield return new WaitForSecondsRealtime(_fadeFullDelay);

            FadeTitle.Value = "";

            while (FadeAmount.Value > 0)
            {
                FadeAmount.Value -= delta;
                FadeColor.Value -= new Color(0, 0, 0, delta);

                yield return new WaitForSecondsRealtime(0.01f);
            }

            FadeAmount.Value = 0;
            FadeColor.Value = new Color(0, 0, 0, 0);

            if (fadeSettings.BlockPlayerWithFade)
            {
                _playerBlocker.UnBlockPlayer();
            }

            IsFading.Value = false;

            if (fadeSettings.OnFadeOutCallback != null)
            {
                fadeSettings.OnFadeOutCallback?.Invoke();
            }
        }
    }

    public class FadeSettings
    {
        public string FadeTitle;
        public bool BlockPlayerWithFade;
        public Action OnFadeInCallback;
        public Action OnFadeOutCallback;
    }
}
