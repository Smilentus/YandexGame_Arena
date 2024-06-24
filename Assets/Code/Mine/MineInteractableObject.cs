using Dimasyechka.Code.BaseInteractionSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.Mine
{
    public class MineInteractableObject : BaseInteractable
    {
        [SerializeField]
        private Mine _mine;


        private MineMiniGameViewModel _mineMiniGameViewModel;

        [Inject]
        public void Construct(MineMiniGameViewModel mineMiniGameViewModel)
        {
            _mineMiniGameViewModel = mineMiniGameViewModel;
        }


        public override void OnInteractionStarted()
        {
            _mineMiniGameViewModel.SetMiniGame(new MineMiniGame(_mine));
        }

        public override void OnInteractionEnded()
        {
            _mineMiniGameViewModel.Hide();
        }
    }
}
