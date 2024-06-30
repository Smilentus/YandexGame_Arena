using Dimasyechka.Code.PlayerSystem;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class PlayerGameProgress : MonoBehaviour
    {
        [SerializeField]
        private List<PlayerProgressMilestone> _progressMilestones = new List<PlayerProgressMilestone>();
        public List<PlayerProgressMilestone> ProgressMilestones => _progressMilestones;


        private ReactiveProperty<double> _playerProgress = new ReactiveProperty<double>();
        public ReactiveProperty<double> PlayerProgress => _playerProgress;


        public double MaxAchievableProgress => ProgressMilestones[^1].Value;


        private RuntimePlayerObject _runtimePlayerObject;
        private AchievementsController _achievementsController;

        [Inject]
        public void Construct(
            RuntimePlayerObject runtimePlayerObject,
            AchievementsController achievementsController)
        {
            _runtimePlayerObject = runtimePlayerObject;
            _achievementsController = achievementsController;
        }


        private void Awake()
        {
            LoadMilestones();
        }


        private void FixedUpdate()
        {
            CheckPlayerProgress();
        }


        private void LoadMilestones()
        {
            for (int i = 0; i < _progressMilestones.Count; i++)
            {
                if (_achievementsController.IsCompletedAchievement(_progressMilestones[i].Achievement.Guid))
                {
                    _progressMilestones[i].IsCompleted.Value = true;
                }
            }
        }

        private void CheckPlayerProgress()
        {
            if (_runtimePlayerObject.RuntimePlayerStats.TotalPower > _playerProgress.Value)
            {
                _playerProgress.Value = _runtimePlayerObject.RuntimePlayerStats.TotalPower;

                for (int i = 0; i < _progressMilestones.Count; i++)
                {
                    if (_progressMilestones[i].IsCompleted.Value) continue;

                    if (_playerProgress.Value >= _progressMilestones[i].Value)
                    {
                        SetMilestone(_progressMilestones[i]);
                    }
                }
            }
        }

        private void SetMilestone(PlayerProgressMilestone milestone)
        {
            _runtimePlayerObject.RuntimePlayerStats.PlayerPrefix = milestone.PlayerPrefix;
            milestone.IsOpened.Value = true;
        }

        public void CompleteMilestoneAchievement(PlayerProgressMilestone milestone)
        {
            milestone.IsOpened.Value = false;
            milestone.IsCompleted.Value = true;

            _achievementsController.CompleteAchievement(milestone.Achievement);
        }
    }


    [System.Serializable]
    public class PlayerProgressMilestone
    {
        public string PlayerPrefix;
        public double Value;
        public AchievementProfile Achievement;

        [HideInInspector]
        public ReactiveProperty<bool> IsCompleted = new ReactiveProperty<bool>();

        [HideInInspector]
        public ReactiveProperty<bool> IsOpened = new ReactiveProperty<bool>();
    }
}
