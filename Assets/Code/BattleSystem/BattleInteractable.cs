using Dimasyechka.Code.BaseInteractionSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public class BattleInteractable : BaseInteractable
    {
        [SerializeField]
        private BattleSettings _battleSettings;


        private BattleController _battleController;

        [Inject]
        public void Construct(BattleController battleController)
        {
            _battleController = battleController;
        }


        public override void OnInteractionStarted()
        {
            if (_battleController.IsBattleInProgress) return;

            _battleController.StartBattle(_battleSettings);
        }
    }
}
