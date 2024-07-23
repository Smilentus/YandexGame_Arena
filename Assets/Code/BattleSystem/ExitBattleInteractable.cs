using System;
using Dimasyechka.Code.BaseInteractionSystem;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public class ExitBattleInteractable : BaseInteractable
    {
        private BattleController _battleController;

        [Inject]
        public void Construct(BattleController battleController)
        {
            _battleController = battleController;
        }


        private void Awake()
        {
            IsInteractable = false;
        }


        public override void OnInteractionStarted()
        {
            _battleController.LeaveBattle();
            IsInteractable = false;
        }
    }
}
