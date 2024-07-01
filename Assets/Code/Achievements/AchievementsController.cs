using Dimasyechka.Code.PlayerSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class AchievementsController : MonoBehaviour
    {
        private List<string> _completedAchievements = new List<string>();


        private RuntimePlayerUpgrader _statsUpgrader;
        private RewardsController _rewardController;

        [Inject]
        public void Construct(
            RuntimePlayerUpgrader statsUpgrader,
            RewardsController rewardController)
        {
            _rewardController = rewardController;
            _statsUpgrader = statsUpgrader;
        }


        public bool IsCompletedAchievement(string achievementGuid) => _completedAchievements.Contains(achievementGuid);


        public void CompleteAchievement(AchievementProfile profile)
        {
            if (_completedAchievements.Contains(profile.Guid))
            {
                Debug.Log($"Trying to complete achievement that has been already achieved");
                return;
            }

            _completedAchievements.Add(profile.Guid);

            // TODO: В идеале обрабатывать момент получше, чтобы сохранять неполученные награды и выдавать их позже
            if (profile.RewardHandler == null)
            {
                Debug.LogError($"Achievement Reward Handler is equals null");
            }
            else
            {
                _statsUpgrader.SetupRewardHandler(ref profile.RewardHandler);

                _rewardController.ClaimReward(
                    profile.RewardHandler,
                    new List<object>()
                    {
                        profile
                    }
                );
            }

            Save();
        }

        private void Save()
        {

        }
    }
}
