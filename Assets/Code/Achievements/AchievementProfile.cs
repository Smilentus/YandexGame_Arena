using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "AchievementProfile", menuName = "Achievements/New AchievementProfile")]
    public class AchievementProfile : ScriptableObject
    {
        [field: SerializeField]
        public RewardHandler RewardHandler;


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
