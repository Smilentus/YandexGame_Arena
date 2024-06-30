using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "LegsRewardHandler", menuName = "Achievements/RewardHandlers/New LegsRewardHandler")]
    public class LegsRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward(AchievementProfile profile)
        {
            if (profile is AchievementWithValueProfile)
            {
                _runtimePlayerObject.RuntimePlayerStats.LegsPower += (profile as AchievementWithValueProfile).Value;
            }
        }
    }
}
