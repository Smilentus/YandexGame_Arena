using UnityEngine;

namespace Dimasyechka.Code.Achievements.RewardHandlers
{
    public class AchievementsRewardHandler : MonoBehaviour
    {
        //[SerializeField]
        //private List<GameObject> _handlersGameObjects = new List<GameObject>();


        //private List<IRewardHandler> _rewardHandlers = new List<IRewardHandler>();


        //private void Awake()
        //{
        //    foreach (var handler in _handlersGameObjects)
        //    {
        //        if (handler.TryGetComponent<IRewardHandler>(out IRewardHandler rewardHandler))
        //        {
        //            _rewardHandlers.Add(rewardHandler);
        //        }
        //    }
        //}


        //public void RewardAchievement(string achievementGuid)
        //{
        //    foreach (IRewardHandler handler in _rewardHandlers)
        //    {
        //        handler.TryReward(achievementGuid);
        //    }
        //}


        //[ContextMenu("Get Handlers From Children")]
        //private void GetHandlersFromChildren()
        //{
        //    _handlersGameObjects.Clear();

        //    for (int i = 0; i < this.transform.childCount; i++)
        //    {
        //        _handlersGameObjects.Add(this.transform.GetChild(i).gameObject);
        //    }
        //}
    }

    public abstract class AchievementWithValueHandler : BaseRewardHandler<AchievementWithValueProfile> { }

    public abstract class BaseRewardHandler<T> : ScriptableObject
        where T : AchievementProfile
    {
        public T Profile;

        public void TryReward(string achievementGuid)
        {
            if (Profile.Guid == achievementGuid)
            {
                Reward();
            }
        }

        protected abstract void Reward();
    }

    public interface IRewardHandler
    {
        public void TryReward();
    }
}
