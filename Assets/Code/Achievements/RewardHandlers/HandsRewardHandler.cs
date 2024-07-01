using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "HandsRewardHandler", menuName = "Achievements/RewardHandlers/New HandsRewardHandler")]
    public class HandsRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward()
        {
            _runtimePlayerObject.RuntimePlayerStats.HandsPower += _rewardValue;
        }
    }
}
