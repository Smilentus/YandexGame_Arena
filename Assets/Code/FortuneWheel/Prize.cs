using UnityEngine;

namespace Dimasyechka
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
    }
}
