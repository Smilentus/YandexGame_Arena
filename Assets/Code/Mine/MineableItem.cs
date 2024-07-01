using UnityEngine;

namespace Dimasyechka.Code.Mine
{
    [CreateAssetMenu(fileName = "MineableItem", menuName = "Create/New MineableItem")]
    public class MineableItem : ScriptableObject
    {
        [field: SerializeField]
        public string Guid;


        [field: SerializeField]
        public string Title;


        [field: SerializeField]
        public int Durability;


        [field: SerializeField]
        public Sprite Icon;


        [field: SerializeField]
        public Sprite MineableIcon;


        [field: SerializeField]
        public uint RewardValue;
    }
}
