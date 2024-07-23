using UnityEngine;

namespace Dimasyechka.Code.BattleSystem.Gates
{
    public class LobbyGates : BaseGates
    {
        [SerializeField]
        private ExitBattleInteractable _interactable;


        protected override void OnBattleStarted()
        {
            _gatesObject.SetActive(true);
            _interactable.IsInteractable = true;
        }

        protected override void OnBattleEnded()
        {
            _gatesObject.SetActive(false);
            _interactable.IsInteractable = false;
        }
    }
}
