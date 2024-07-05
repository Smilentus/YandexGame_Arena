using UnityEngine;

namespace Dimasyechka.Code.Achievements
{
    [CreateAssetMenu(fileName = "AchievementWithValueProfile", menuName = "Achievements/New AchievementWithValueProfile")]
    public class AchievementWithValueProfile : AchievementProfile
    {
        [field: SerializeField]
        public double Value;
    }
}