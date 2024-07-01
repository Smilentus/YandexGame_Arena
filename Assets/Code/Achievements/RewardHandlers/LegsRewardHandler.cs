using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "LegsRewardHandler", menuName = "Achievements/RewardHandlers/New LegsRewardHandler")]
    public class LegsRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward()
        {
            _runtimePlayerObject.RuntimePlayerStats.LegsPower += _rewardValue;
        }
    }
}
