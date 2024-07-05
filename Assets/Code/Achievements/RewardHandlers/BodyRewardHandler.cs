using UnityEngine;

namespace Dimasyechka.Code.Achievements.RewardHandlers
{
    [CreateAssetMenu(fileName = "BodyRewardHandler", menuName = "Achievements/RewardHandlers/New BodyRewardHandler")]
    public class BodyRewardHandler : BasePlayerStatsRewardHandler
    {
        public override void Reward()
        {
            _runtimePlayerObject.RuntimePlayerStats.BodyPower += _rewardValue;
        }
    }
}
