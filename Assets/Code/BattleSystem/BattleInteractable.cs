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
        private EnemyInspectionViewModel _enemyInspectionViewModel;

        [Inject]
        public void Construct(BattleController battleController, EnemyInspectionViewModel enemyInspectionViewModel)
        {
            _battleController = battleController;
            _enemyInspectionViewModel = enemyInspectionViewModel;
        }


        private void Awake()
        {
            _battleController.onBattleStarted += OnBattleStarted;
            _battleController.onBattleEnded += OnBattleEnded;
        }

        private void OnDestroy()
        {
            _battleController.onBattleStarted -= OnBattleStarted;
            _battleController.onBattleEnded -= OnBattleEnded;
        }


        private void OnBattleStarted()
        {
            IsInteractable = false;
        }

        private void OnBattleEnded()
        {
            IsInteractable = true;
        }


        public override void OnInteractionStarted()
        {
            if (_battleController.IsBattleInProgress) return;

            // Показываем информацию о противнике?
            _enemyInspectionViewModel.SetupModel(_battleSettings);
            _enemyInspectionViewModel.Show();
        }
    }
}
