using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "AchievementWithValueProfile", menuName = "Achievements/New AchievementWithValueProfile")]
    public class AchievementWithValueProfile : AchievementProfile
    {
        [field: SerializeField]
        public double Value;
    }
}