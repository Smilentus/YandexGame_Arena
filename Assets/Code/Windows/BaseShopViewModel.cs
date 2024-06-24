using Dimasyechka.Code.Utilities;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using Zenject;

namespace Dimasyechka.Code.Windows
{
    public class BaseShopViewModel<T> : MonoViewModel<T>
    {
        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsVisible = new ReactiveProperty<bool>();


        private PlayerBlocker _playerBlocker;


        [Inject]
        public void Construct(PlayerBlocker playerBlocker)
        {
            _playerBlocker = playerBlocker;
        }


        [RxAdaptableMethod]
        public void Show()
        {
            IsVisible.Value = true;

            OnDrawUI();

            _playerBlocker.BlockPlayer();

            CursorController.Instance.RegisterCursorController(this.name);

            OnShow();
        }

        [RxAdaptableMethod]
        public void Hide()
        {
            IsVisible.Value = false;

            _playerBlocker.UnBlockPlayer();

            CursorController.Instance.UnRegisterCursorController(this.name);

            OnHide();
        }



        public virtual void OnShow() { }
        public virtual void OnHide() { }

        public virtual void OnDrawUI() { }
    }
}
