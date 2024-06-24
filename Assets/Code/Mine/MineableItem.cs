using UnityEngine;

namespace Dimasyechka.Code.Mine
{
    [CreateAssetMenu(fileName = "MineableItem", menuName = "Create/New MineableItem")]
    public class MineableItem : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; set; }


        [field: SerializeField]
        public int Durability { get; set; }


        [field: SerializeField]
        public Sprite Icon { get; set; }


        [field: SerializeField]
        public Sprite MineableIcon { get; set; }
    }
}
