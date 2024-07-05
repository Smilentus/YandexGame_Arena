using UnityEngine;

namespace Dimasyechka.Code.Utilities
{
    public class RotatableToMainCameraObject : MonoBehaviour
    {
        private void FixedUpdate()
        {
            this.transform.LookAt(Camera.main.transform);
        }
    }
}
