using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class AchievementsMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private AchievementsController _achievementsController;

        [SerializeField]
        private PlayerGameProgress _playerGameProgress;


        public override void InstallBindings()
        {
            BindPlayerGameProgress();
            BindAchievementsController();
        }

        private void BindPlayerGameProgress()
        {
            Container.Bind<PlayerGameProgress>().FromInstance(_playerGameProgress).AsSingle();
            Container.Bind<PlayerProgressMilestoneViewModelFactory>().FromNew().AsSingle();
        }

        private void BindAchievementsController()
        {
            Container.Bind<AchievementsController>().FromInstance(_achievementsController).AsSingle();
        }
    }
}
