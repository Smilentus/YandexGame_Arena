using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem.Gates
{
    public abstract class BaseGates : MonoBehaviour
    {
        [SerializeField]
        protected GameObject _gatesObject;

        [SerializeField]
        protected bool _defaultActiveState;


        protected BattleController _battleController;

        [Inject]
        public void Construct(BattleController battleController)
        {
            _battleController = battleController;
        }


        protected virtual void Awake()
        {
            _battleController.onBattleStarted += OnBattleStarted;
            _battleController.onBattleEnded += OnBattleEnded;
        }

        protected virtual void OnDestroy()
        {
            _battleController.onBattleStarted -= OnBattleStarted;
            _battleController.onBattleEnded -= OnBattleEnded;
        }

        protected virtual void Start()
        {
            _gatesObject.SetActive(_defaultActiveState);
        }


        protected abstract void OnBattleStarted();
        protected abstract void OnBattleEnded();
    }
}
