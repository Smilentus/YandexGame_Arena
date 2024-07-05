using Dimasyechka.Code.Rewards;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.Achievements
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
            BindRewardsController();
        }

        private void BindRewardsController()
        {
            Container.BindInterfacesAndSelfTo<RewardsController>().AsSingle();
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
