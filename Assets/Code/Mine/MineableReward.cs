using Dimasyechka.Code.Mine;
using UnityEngine;

namespace Dimasyechka
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
