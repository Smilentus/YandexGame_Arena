using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "BodyRewardHandler", menuName = "Achievements/RewardHandlers/New BodyRewardHandler")]
    public class BodyRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward(AchievementProfile profile)
        {
            if (profile is AchievementWithValueProfile)
            {
                _runtimePlayerObject.RuntimePlayerStats.BodyPower += (profile as AchievementWithValueProfile).Value;
            }
        }
    }
}
