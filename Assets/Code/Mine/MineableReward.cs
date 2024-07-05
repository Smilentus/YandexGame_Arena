using UnityEngine;

namespace Dimasyechka.Code.Mine
{
    [CreateAssetMenu(fileName = "MineableReward", menuName = "Mineable/New MineableReward")]
    public class MineableReward : ScriptableObject
    {
        [field: SerializeField]
        public MineableItem Mineable;

        [field: SerializeField]
        public double RewardValue;
    }
}
