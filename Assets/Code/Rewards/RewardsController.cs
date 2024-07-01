using System.Collections.Generic;

namespace Dimasyechka
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
