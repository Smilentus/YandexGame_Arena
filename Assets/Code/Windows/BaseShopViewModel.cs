using Dimasyechka.Code.Utilities;
using Dimasyechka.Lubribrary.RxMV.Core;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.Windows
{
    public class BaseShopViewModel<T> : MonoViewModel<T>
    {
        [SerializeField]
        private bool _blockUserController = true;

        [SerializeField]
        private bool _registerCursor = true;
        

        [RxAdaptableProperty]
        public ReactiveProperty<bool> IsVisible = new ReactiveProperty<bool>();


        private PlayerBlocker _playerBlocker;


        [Inject]
        public void Construct(PlayerBlocker playerBlocker)
        {
            _playerBlocker = playerBlocker;
        }


        [RxAdaptableMethod]
        public virtual void Show()
        {
            IsVisible.Value = true;

            OnDrawUI();

            if (_blockUserController)
                _playerBlocker.BlockPlayer();

            if (_registerCursor)
                CursorController.Instance.RegisterCursorController(this.name);

            OnShow();
        }

        [RxAdaptableMethod]
        public virtual void Hide()
        {
            IsVisible.Value = false;

            if (_blockUserController) 
                _playerBlocker.UnBlockPlayer();

            if (_registerCursor)
                CursorController.Instance.UnRegisterCursorController(this.name);

            OnHide();
        }


        public virtual void OnShow() { }
        public virtual void OnHide() { }

        public virtual void OnDrawUI() { }
    }
}
