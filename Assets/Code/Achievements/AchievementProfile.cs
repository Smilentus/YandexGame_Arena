using Dimasyechka.Code.Achievements.RewardHandlers;
using UnityEngine;

namespace Dimasyechka.Code.Achievements
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
