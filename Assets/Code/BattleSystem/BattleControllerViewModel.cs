using Dimasyechka.Code.HealthSystem;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.Extensions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public class BattleControllerViewModel : MonoViewModel<BattleController>
    {
        [SerializeField]
        private HealthComponentViewModel _healthViewModel;

        [SerializeField]
        private DamageComponentViewModel _damageViewModel;


        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsBattleInProgress = new ReactiveProperty<bool>();

        [RxAdaptableProperty]
        public ReactiveProperty<string> BattleTimeLabel = new ReactiveProperty<string>();

        [RxAdaptableProperty]
        public ReactiveProperty<int> EnemiesLeft = new ReactiveProperty<int>();


        [RxAdaptableProperty]
        public ReactiveProperty<bool> PlayerWinPanelEnabled = new ReactiveProperty<bool>();

        [RxAdaptableProperty]
        public ReactiveProperty<bool> PlayerLosePanelEnabled = new ReactiveProperty<bool>();


        [Inject]
        public void Construct(BattleController battleController)
        {
            ZenjectModel(battleController);
        }


        protected override void OnSetupModel()
        {
            _disposablesStorage.AddToDisposables(Model.EnemiesInBattle.SubscribeToEachOther(EnemiesLeft));

            _disposablesStorage.AddToDisposables(Model.BattleTimeLeft.Subscribe(x =>
            {
                BattleTimeLabel.Value = x.ToString(@"mm\:ss");
            }));

            Model.onBattleStarted += OnBattleStarted;
            Model.onBattleEnded += OnBattleEnded;
            Model.onPlayerWin += OnPlayerWin;
            Model.onPlayerLose += OnPlayerLose;
        }

        protected override void OnRemoveModel()
        {
            Model.onBattleStarted -= OnBattleStarted;
            Model.onBattleEnded -= OnBattleEnded;
            Model.onPlayerWin -= OnPlayerWin;
            Model.onPlayerLose -= OnPlayerLose;
        }


        private void OnPlayerLose()
        {
            PlayerLosePanelEnabled.Value = true;
        }

        private void OnPlayerWin()
        {
            PlayerWinPanelEnabled.Value = true;
        }


        private void OnBattleStarted()
        {
            IsBattleInProgress.Value = true;

            _healthViewModel.SetupModel(Model.InstantiatedPlayerCharacter.Health);
            _damageViewModel.SetupModel(Model.InstantiatedPlayerCharacter.Damage);
        }

        private void OnBattleEnded()
        {
            PlayerWinPanelEnabled.Value = false;
            PlayerLosePanelEnabled.Value = false;

            IsBattleInProgress.Value = false;
        }
    }
}
