using UnityEngine.EventSystems;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class UsablePlayerBoosterViewModel : PlayerBoosterViewModel, IPointerClickHandler
    {
        private PlayerBoosterInteractionWindow _window;

        private int _slotIndex;


        [Inject]
        public void Construct(PlayerBoosterInteractionWindow window)
        {
            _window = window;
        }


        public void SetSlotIndex(int idx)
        {
            _slotIndex = idx;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            _window.SetupModel(Model);
            _window.SetUnEquipInteractions(_slotIndex);
            _window.Show();
        }
    }
}
