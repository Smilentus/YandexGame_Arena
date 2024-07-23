using System;
using UnityEngine;

namespace Dimasyechka.Code.Triggerables
{
    public class TriggerableObject : MonoBehaviour
    {
        public event Action<Collider> onTriggerEnter;
        public event Action<Collider> onTriggerExit;
        public event Action<Collider> onTriggerStay;
        

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExit?.Invoke(other);
        }

        private void OnTriggerStay(Collider other)
        {
            onTriggerStay?.Invoke(other);
        }
    }
}
