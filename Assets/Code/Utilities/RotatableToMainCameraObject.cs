using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimasyechka
{
    public class RotatableToMainCameraObject : MonoBehaviour
    {
        private void FixedUpdate()
        {
            this.transform.LookAt(Camera.main.transform);
        }
    }
}
