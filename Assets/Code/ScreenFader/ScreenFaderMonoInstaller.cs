using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class ScreenFaderMonoInstaller : MonoInstaller
    {
        [SerializeField]
        private GeneralScreenFader _generalScreenFader;


        public override void InstallBindings()
        {
            BindGeneralScreenFader();
        }

        private void BindGeneralScreenFader()
        {
            Container.Bind<GeneralScreenFader>().FromInstance(_generalScreenFader).AsSingle();
        }
    }
}
