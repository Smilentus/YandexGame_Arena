using UnityEngine;

namespace Dimasyechka.Code.Achievements.RewardHandlers
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
