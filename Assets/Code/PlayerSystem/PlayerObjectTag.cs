using UnityEngine;

namespace Dimasyechka.Code.PlayerSystem
{
    public class PlayerObjectTag : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        public Animator AnimatorReference => _animator;
    }
}
