using UnityEngine;

namespace Dimasyechka.Code.BoostingSystem
{
    [CreateAssetMenu(fileName = "PlayerBooster", menuName = "PlayerBoosters/Create PlayerBooster")]
    public class PlayerBooster : ScriptableObject
    {
        [field: SerializeField]
        public string Guid { get; set; }


        [field: SerializeField]
        public string Title { get; set; }


        [field: SerializeField]
        public Sprite Icon { get; set; }


        [field: SerializeField]
        public double Value { get; set; }

        
        [field: SerializeField]
        public Color RareColor { get; set; }
    }
}
