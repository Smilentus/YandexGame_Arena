using Dimasyechka.Code.BaseInteractionSystem;
using Dimasyechka.Code.PlayerSystem;
using Dimasyechka.Code.ScreenFader;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.Teleportation
{
    public class TeleportPoint : BaseInteractable
    {
        [SerializeField]
        private string _teleportationTitle = "Перемещение";

        [SerializeField]
        private Transform _endPoint;


        private PlayerObjectTag _playerObjectTag;
        private GeneralScreenFader _generalScreenFader;

        [Inject]
        public void Construct(
            PlayerObjectTag playerObjectTag,
            GeneralScreenFader generalScreenFader)
        {
            _generalScreenFader = generalScreenFader;
            _playerObjectTag = playerObjectTag;
        }


        public override void OnInteractionStarted()
        {
            _generalScreenFader.FadeInAndOut(new FadeSettings()
            {
                FadeTitle = _teleportationTitle,
                BlockPlayerWithFade = true,
                OnFadeInCallback = FadeInCallback,
                OnFadeOutCallback = FadeOutCallback
            });
        }


        private void FadeInCallback()
        {
            _playerObjectTag.transform.position = _endPoint.position;
            _playerObjectTag.transform.rotation = _endPoint.rotation;
        }

        private void FadeOutCallback()
        {
            // ...
        }
    }
}
