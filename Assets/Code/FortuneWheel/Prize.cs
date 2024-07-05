using UnityEngine;

namespace Dimasyechka.Code.FortuneWheel
{
    [CreateAssetMenu(fileName = "FortuneWheelPrize", menuName = "FortuneWheel/FortuneWheelPrize")]
    public class Prize : ScriptableObject
    {
        [field: SerializeField]
        public string Guid;


        [field: SerializeField]
        public string Title;


        [field: SerializeField]
        public string Description;


        [field: SerializeField]
        public Sprite Icon;


        [field: SerializeField]
        public float RandomWeight;


        [field: SerializeField]
        public int MinValue;


        [field: SerializeField]
        public int MaxValue;
    }
}
