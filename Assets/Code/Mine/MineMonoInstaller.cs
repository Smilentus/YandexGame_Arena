using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.Mine
{
    public class MineMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private MineMiniGameViewModel _mineMiniGameViewModel;


        public override void InstallBindings()
        {
            BindMineMiniGameViewModel();
            BindMineableItemViewModelFactory();
        }

        private void BindMineableItemViewModelFactory()
        {
            Container.Bind<MineableItemViewModelFactory>().FromNew().AsSingle();
        }

        private void BindMineMiniGameViewModel()
        {
            Container.Bind<MineMiniGameViewModel>().FromInstance(_mineMiniGameViewModel).AsSingle();
        }
    }
}
