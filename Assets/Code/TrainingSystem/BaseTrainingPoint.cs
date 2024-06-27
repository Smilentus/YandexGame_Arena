using Dimasyechka.Code.BaseInteractionSystem;
using Dimasyechka.Code.BoostingSystem;
using Dimasyechka.Code.PlayerSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class BaseTrainingPoint : BaseInteractable
    {
        public enum TrainingPart { Body, Hands, Legs }

        [SerializeField]
        private TrainingPart _trainingPart;


        private RuntimePlayerObject _runtimePlayerObject;
        private PlayerBoostersContainer _boosterContainer;

        private BaseTrainingViewModel _trainingViewModel;

        [Inject]
        public void Construct(
            RuntimePlayerObject runtimePlayerObject,
            PlayerBoostersContainer boosterContainer,
            BaseTrainingViewModel trainingViewModel)
        {
            _runtimePlayerObject = runtimePlayerObject;
            _boosterContainer = boosterContainer;

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
                    _runtimePlayerObject.RuntimePlayerStats.BodyPower += _boosterContainer.GetMultiplicationFactor();
                    break;
                case TrainingPart.Hands:
                    _runtimePlayerObject.RuntimePlayerStats.HandsPower += _boosterContainer.GetMultiplicationFactor();
                    break;
                case TrainingPart.Legs:
                    _runtimePlayerObject.RuntimePlayerStats.LegsPower += _boosterContainer.GetMultiplicationFactor();
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
