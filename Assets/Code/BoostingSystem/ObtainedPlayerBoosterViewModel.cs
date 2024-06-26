using UnityEngine.EventSystems;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class ObtainedPlayerBoosterViewModel : PlayerBoosterViewModel, IPointerClickHandler
    {
        private PlayerBoosterInteractionWindow _window;

        [Inject]
        public void Construct(PlayerBoosterInteractionWindow window)
        {
            _window = window;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            _window.SetupModel(Model);
            _window.Show();
        }
    }
}
