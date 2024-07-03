using Dimasyechka.Code.BattleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class BattleMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private BattleController _battleController;


        public override void InstallBindings()
        {
            BindBattleController();
        }

        private void BindBattleController()
        {
            Container.Bind<BattleController>().FromInstance(_battleController).AsSingle();
            Container.Bind<RuntimeBattleCharacterFactory>().FromNew().AsSingle();
        }
    }
}
