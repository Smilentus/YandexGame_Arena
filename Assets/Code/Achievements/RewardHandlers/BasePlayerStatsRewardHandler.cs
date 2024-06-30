using Dimasyechka.Code.PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Dimasyechka
{
    public abstract class RewardHandler : ScriptableObject
    {
        public abstract void Setup(List<object> setupObjects);
        public abstract void Reward(AchievementProfile profile);
    }

    public abstract class BasePlayerStatsRewardHandler : RewardHandler
    {
        protected RuntimePlayerObject _runtimePlayerObject;

        public override void Setup(List<object> setupObjects)
        {
            foreach (object obj in setupObjects)
            {
                if (obj.GetType().Equals(typeof(RuntimePlayerObject)))
                {
                    _runtimePlayerObject = obj as RuntimePlayerObject;
                }
            }
        }
    }
}
