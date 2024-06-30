using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "HandsRewardHandler", menuName = "Achievements/RewardHandlers/New HandsRewardHandler")]
    public class HandsRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward(AchievementProfile profile)
        {
            if (profile is AchievementWithValueProfile)
            {
                _runtimePlayerObject.RuntimePlayerStats.HandsPower += (profile as AchievementWithValueProfile).Value;
            }
        }
    }
}
