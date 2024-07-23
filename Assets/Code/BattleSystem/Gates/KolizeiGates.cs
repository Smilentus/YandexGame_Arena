using Dimasyechka.Code.Triggerables;
using UnityEngine;

namespace Dimasyechka.Code.BattleSystem.Gates
{
    public class KolizeiGates : BaseGates
    {
        [SerializeField]
        private TriggerableObject _trigger;


        private bool _waitForClosing;


        private void OnEnable()
        {
            _trigger.onTriggerEnter += OnTriggerEnterHandler;

            _battleController.onBattleLeft += OnBattleLeft;
        }

        private void OnDisable()
        {
            _trigger.onTriggerEnter -= OnTriggerEnterHandler;

            _battleController.onBattleLeft -= OnBattleLeft;
        }


        private void OnTriggerEnterHandler(Collider other)
        {
            if (!_waitForClosing) return;

            if (other.CompareTag("Player")) // Any method you like maybe try to check layers
            {
                _waitForClosing = false;
                _gatesObject.SetActive(true);
            }
        }


        private void OnBattleLeft()
        {
            _waitForClosing = false;
            _gatesObject.SetActive(true);
        }


        protected override void OnBattleStarted()
        {
            _waitForClosing = false;
            _gatesObject.SetActive(false);
        }

        protected override void OnBattleEnded()
        {
            _waitForClosing = true;
        }
    }
}
