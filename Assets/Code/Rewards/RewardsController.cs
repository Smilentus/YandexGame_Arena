using System.Collections.Generic;
using Dimasyechka.Code.Achievements.RewardHandlers;

namespace Dimasyechka.Code.Rewards
{
    public class RewardsController
    {
        public void ClaimReward(RewardHandler rewardHandler, List<object> setupObjects = null)
        {
            rewardHandler.Setup(setupObjects);
            rewardHandler.Reward();
        }
    }
}
