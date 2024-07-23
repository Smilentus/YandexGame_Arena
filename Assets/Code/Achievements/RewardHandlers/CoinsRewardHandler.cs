using UnityEngine;

namespace Dimasyechka.Code.Achievements.RewardHandlers
{
    [CreateAssetMenu(fileName = "CoinsRewardHandler", menuName = "Achievements/RewardHandlers/New CoinsRewardHandler")]
    public class CoinsRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward()
        {
            _runtimePlayerObject.RuntimePlayerStats.Coins += (uint)_rewardValue;
        }
    }
}
