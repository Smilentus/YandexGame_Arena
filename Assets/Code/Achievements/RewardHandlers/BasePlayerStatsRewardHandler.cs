using Dimasyechka.Code.PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Dimasyechka
{
    public abstract class RewardHandler : ScriptableObject
    {
        public abstract void Setup(List<object> setupObjects);
        public abstract void Reward();
    }

    public abstract class BasePlayerStatsRewardHandler : RewardHandler
    {
        protected RuntimePlayerObject _runtimePlayerObject;
        protected double _rewardValue;

        public override void Setup(List<object> setupObjects)
        {
            foreach (object obj in setupObjects)
            {
                if (obj.GetType().Equals(typeof(RuntimePlayerObject)))
                {
                    _runtimePlayerObject = obj as RuntimePlayerObject;
                }

                if (obj.GetType().Equals(typeof(AchievementWithValueProfile)))
                {
                    _rewardValue = (obj as AchievementWithValueProfile).Value;
                }

                if (obj.GetType().Equals(typeof(double)))
                {
                    _rewardValue = (double)obj;
                }
            }
        }
    }
}
