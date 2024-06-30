using Dimasyechka.Code.PlayerSystem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class AchievementsController : MonoBehaviour
    {
        private List<string> _completedAchievements = new List<string>();


        private RuntimePlayerObject _runtimePlayerObject;

        [Inject]
        public void Construct(RuntimePlayerObject runtimePlayerObject)
        {
            _runtimePlayerObject = runtimePlayerObject;
        }


        public bool IsCompletedAchievement(string achievementGuid) => _completedAchievements.Contains(achievementGuid);


        public void CompleteAchievement(AchievementProfile profile)
        {
            if (_completedAchievements.Contains(profile.Guid))
            {
                Debug.Log($"Trying to complete achievement that has been already achieved");
                return;
            }

            _completedAchievements.Add(profile.Guid);

            // TODO: В идеале обрабатывать момент получше, чтобы сохранять неполученные награды и выдавать их позже
            if (profile.RewardHandler == null)
            {
                Debug.LogError($"Achievement Reward Handler is equals null");
            }
            else
            {
                profile.RewardHandler.Setup(new List<object>() { _runtimePlayerObject });
                profile.RewardHandler.Reward(profile);
            }

            Save();
        }

        private void Save()
        {

        }
    }
}
