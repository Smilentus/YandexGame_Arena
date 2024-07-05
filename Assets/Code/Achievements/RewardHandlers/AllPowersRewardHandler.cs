using UnityEngine;

namespace Dimasyechka.Code.Achievements.RewardHandlers
{
    [CreateAssetMenu(fileName = "AllPowersRewardHandler", menuName = "Achievements/RewardHandlers/New AllPowersRewardHandler")]
    public class AllPowersRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward()
        {
            _runtimePlayerObject.RuntimePlayerStats.LegsPower += _rewardValue;
            _runtimePlayerObject.RuntimePlayerStats.HandsPower += _rewardValue;
            _runtimePlayerObject.RuntimePlayerStats.BodyPower += _rewardValue;
        }
    }
}
