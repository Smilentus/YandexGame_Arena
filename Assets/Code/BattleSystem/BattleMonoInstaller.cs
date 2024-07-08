using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public class BattleMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private BattleController _battleController;


        public override void InstallBindings()
        {
            BindBattleController();
            BindWorldFloatingObject();
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
    }
}
