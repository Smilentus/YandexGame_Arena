using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.TrainingSystem
{
    public class TrainingMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private BaseTrainingViewModel _baseTrainingViewModel;

        public override void InstallBindings()
        {
            BindTrainingViewModel();
        }

        private void BindTrainingViewModel()
        {
            Container.Bind<BaseTrainingViewModel>().FromInstance(_baseTrainingViewModel).AsSingle();
        }
    }
}
