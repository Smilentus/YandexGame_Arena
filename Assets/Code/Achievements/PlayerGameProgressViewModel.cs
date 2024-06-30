using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Lubribrary.Extensions;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class PlayerGameProgressViewModel : MonoViewModel<PlayerGameProgress>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<double> PlayerGameProgress = new ReactiveProperty<double>();

        [RxAdaptableProperty]
        public ReactiveProperty<float> ProgressRatio = new ReactiveProperty<float>();


        [SerializeField]
        private PlayerProgressMilestoneViewModel _milestoneViewModelPrefab;

        [SerializeField]
        private Transform _milestonesContentParent;


        private RectTransform _rectMilestonesContentParent;
        protected RectTransform _milestonesContentParentRect
        {
            get
            {
                if (_rectMilestonesContentParent == null)
                {
                    _rectMilestonesContentParent = _milestonesContentParent.GetComponent<RectTransform>();
                }

                return _rectMilestonesContentParent;
            }
        }


        private PlayerProgressMilestoneViewModelFactory _factory;

        [Inject]
        public void Construct(
            PlayerGameProgress playerGameProgress,
            PlayerProgressMilestoneViewModelFactory factory)
        {
            _factory = factory;

            ZenjectModel(playerGameProgress);
        }


        protected override void OnSetupModel()
        {
            _disposablesStorage.AddToDisposables(Model.PlayerProgress.Subscribe(x =>
            {
                PlayerGameProgress.Value = x;

                CalculateProgressBar();
            }));

            CreateProgressUI();
        }


        private void CreateProgressUI()
        {
            _milestonesContentParent.DestroyChildren();

            for (int i = 0; i < Model.ProgressMilestones.Count; i++)
            {
                PlayerProgressMilestoneViewModel viewModel = _factory.InstantiateForComponent(_milestoneViewModelPrefab.gameObject, _milestonesContentParent);

                viewModel.SetupModel(Model.ProgressMilestones[i]);
                viewModel.onClaimed += ClaimMilestone;

                float posX = (float)_milestonesContentParentRect.rect.width / (float)Model.ProgressMilestones.Count * (i + 1);

                _lastMilestonePos = posX;

                viewModel.transform.localPosition = new Vector3(
                    _milestonesContentParentRect.rect.min.x + posX,
                    viewModel.transform.localPosition.y,
                    viewModel.transform.localPosition.z
                );
            }
        }


        private float _lastMilestonePos = 0;


        private void CalculateProgressBar()
        {
            float generalRatio = Mathf.Clamp01((float)Model.PlayerProgress.Value / (float)Model.MaxAchievableProgress);

            for (int i = 0; i < Model.ProgressMilestones.Count; i++)
            {
                if (Model.PlayerProgress.Value <= Model.ProgressMilestones[i].Value)
                {
                    // 1 - mile = [0:0.1]
                    // 2 - mile = (0.1:0.2]
                    // 3 - mile = (0.2:0.3] etc...
                    float minClamp = (float)Model.PlayerProgress.Value;
                    float maxClamp = (float)Model.ProgressMilestones[i].Value;
                    float startPos = i / (float)Model.ProgressMilestones.Count;
                    generalRatio = Mathf.Clamp01(startPos + (minClamp / maxClamp / Model.ProgressMilestones.Count));
                    break;
                }
            }

            ProgressRatio.Value = generalRatio;
        }

        private void ClaimMilestone(PlayerProgressMilestone milestone)
        {
            Model.CompleteMilestoneAchievement(milestone);
        }
    }


    public class PlayerProgressMilestoneViewModelFactory : DiCreationFactory<PlayerProgressMilestoneViewModel> { }
}
