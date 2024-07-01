using Dimasyechka.Code.BaseInteractionSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class BaseTrainingPoint : BaseInteractable
    {
        public enum TrainingPart { Body, Hands, Legs }

        [SerializeField]
        private TrainingPart _trainingPart;


        private RuntimePlayerUpgrader _statsUpgrader;

        private BaseTrainingViewModel _trainingViewModel;

        [Inject]
        public void Construct(
            RuntimePlayerUpgrader statsUpgrader,
            BaseTrainingViewModel trainingViewModel)
        {
            _statsUpgrader = statsUpgrader;

            _trainingViewModel = trainingViewModel;
        }


        private void OnDestroy()
        {
            _trainingViewModel.onTrainingCompleted -= OnTrainingCompleted;
            _trainingViewModel.onHide -= OnTrainingViewHidden;
        }


        private void OnTrainingViewHidden()
        {
            _trainingViewModel.onTrainingCompleted -= OnTrainingCompleted;
            _trainingViewModel.onHide -= OnTrainingViewHidden;
        }


        private void OnTrainingCompleted()
        {
            switch (_trainingPart)
            {
                case TrainingPart.Body:
                    _statsUpgrader.UpgradeBodyPowerWithMul(1);
                    break;
                case TrainingPart.Hands:
                    _statsUpgrader.UpgradeHandsPowerWithMul(1);
                    break;
                case TrainingPart.Legs:
                    _statsUpgrader.UpgradeLegsPowerWithMul(1);
                    break;
            }
        }


        public override void OnInteractionStarted()
        {
            _trainingViewModel.onTrainingCompleted += OnTrainingCompleted;
            _trainingViewModel.onHide += OnTrainingViewHidden;

            _trainingViewModel.Show();
        }
    }
}
