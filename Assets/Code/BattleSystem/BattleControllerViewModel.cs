using Dimasyechka.Code.BattleSystem;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.Extensions;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class BattleControllerViewModel : MonoViewModel<BattleController>
    {
        [SerializeField]
        private HealthComponentViewModel _healthViewModel;


        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsBattleInProgress = new ReactiveProperty<bool>();

        [RxAdaptableProperty]
        public ReactiveProperty<string> BattleTimeLabel = new ReactiveProperty<string>();

        [RxAdaptableProperty]
        public ReactiveProperty<int> EnemiesLeft = new ReactiveProperty<int>();


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

        }

        private void OnPlayerWin()
        {

        }


        private void OnBattleStarted()
        {
            IsBattleInProgress.Value = true;

            _healthViewModel.SetupModel(Model.InstantiatedPlayerCharacter.Health);
        }

        private void OnBattleEnded()
        {
            IsBattleInProgress.Value = false;
        }
    }
}
