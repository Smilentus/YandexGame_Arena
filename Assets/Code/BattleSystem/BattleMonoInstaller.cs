using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public class BattleMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private BattleController _battleController;

        [SerializeField]
        private EnemyInspectionViewModel _enemyInspectionViewModel;


        public override void InstallBindings()
        {
            BindBattleController();
            BindWorldFloatingObject();
            BindEnemyInspectionViewModel();
        }

        private void BindWorldFloatingObject()
        {
            Container.Bind<WorldFloatingObjectFactory>().FromNew().AsSingle();
        }

        private void BindBattleController()
        {
            Container.Bind<BattleController>().FromInstance(_battleController).AsSingle();
            Container.Bind<RuntimeBattleCharacterFactory>().FromNew().AsSingle();
        }

        private void BindEnemyInspectionViewModel()
        {
            Container.Bind<EnemyInspectionViewModel>().FromInstance(_enemyInspectionViewModel).AsSingle();
        }
    }
}
