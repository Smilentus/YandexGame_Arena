using Dimasyechka.Code.BaseInteractionSystem;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class InteractablePlayerBoostersContainer : BaseInteractable
    {
        private PlayerAvailableBoostersContainerViewModel _containerViewModel;

        [Inject]
        public void Construct(PlayerAvailableBoostersContainerViewModel viewModel)
        {
            _containerViewModel = viewModel;
        }


        public override void OnInteractionStarted()
        {
            _containerViewModel.Show();
        }
    }
}
