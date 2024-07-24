using Dimasyechka.Code.BaseInteractionSystem;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem.Trading
{
    public class BoosterTradingInteractable : BaseInteractable
    {
        private BoosterTradingWindow _window;

        [Inject]
        public void Construct(BoosterTradingWindow window)
        {
            _window = window;
        }


        public override void OnInteractionStarted()
        {
            _window.Show();
        }
    }
}
