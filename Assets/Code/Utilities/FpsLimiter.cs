using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimasyechka
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
