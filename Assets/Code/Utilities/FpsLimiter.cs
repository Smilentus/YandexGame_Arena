using UnityEngine;

namespace Dimasyechka.Code.Utilities
{
    public class FpsLimiter : MonoBehaviour
    {
        [SerializeField]
        private int _targetFrameRate = 60;


        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}
