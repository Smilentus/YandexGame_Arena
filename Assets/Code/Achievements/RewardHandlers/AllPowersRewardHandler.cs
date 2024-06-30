using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "AllPowersRewardHandler", menuName = "Achievements/RewardHandlers/New AllPowersRewardHandler")]
    public class AllPowersRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward(AchievementProfile profile)
        {
            if (profile is AchievementWithValueProfile)
            {
                _runtimePlayerObject.RuntimePlayerStats.LegsPower += (profile as AchievementWithValueProfile).Value;
                _runtimePlayerObject.RuntimePlayerStats.HandsPower += (profile as AchievementWithValueProfile).Value;
                _runtimePlayerObject.RuntimePlayerStats.BodyPower += (profile as AchievementWithValueProfile).Value;
            }
        }
    }
}
