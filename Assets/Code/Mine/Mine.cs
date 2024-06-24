using System.Collections.Generic;
using UnityEngine;

namespace Dimasyechka.Code.Mine
{
    public class Mine : MonoBehaviour
    {
        [SerializeField]
        private List<MineableItem> _mineableItems = new List<MineableItem>();
        public List<MineableItem> MineableItems => _mineableItems;


        public MineableItem GetRandomMineableItem() => _mineableItems[Random.Range(0, _mineableItems.Count)];
    }
}
