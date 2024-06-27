using DG.Tweening;
using UnityEngine;

namespace Dimasyechka
{
    public class UIRotatableObject : MonoBehaviour
    {
        enum RotationAxis { X, Y, Z }

        [Header("Rotation Settings")]
        [SerializeField]
        private float _rotationTime = 0.05f;

        [SerializeField]
        private RotationAxis _rotationAxis = RotationAxis.X;

        [SerializeField]
        private float _rotationMaxAngle = 15f;

        [SerializeField]
        private float _rotationMinAngle = -15f;

        [SerializeField]
        private float _rotationDirection = 1f;

        private Vector3 _rotationValue = Vector3.zero;

        private void Start()
        {
            _rotationValue = this.transform.localEulerAngles;
        }


        private void FixedUpdate()
        {
            CalculateAndSetRotation();
        }


        private void CalculateAndSetRotation()
        {
            float currentAngle = _rotationAxis switch
            {
                RotationAxis.X => _rotationValue.x,
                RotationAxis.Y => _rotationValue.y,
                RotationAxis.Z => _rotationValue.z
            };

            if (currentAngle >= _rotationMaxAngle)
            {
                currentAngle = _rotationMaxAngle;
                _rotationDirection = -1f;
            }

            if (currentAngle <= _rotationMinAngle)
            {
                currentAngle = _rotationMinAngle;
                _rotationDirection = 1f;
            }

            float newAngle = Time.fixedDeltaTime * _rotationTime * _rotationDirection;
            
            _rotationValue = _rotationAxis switch {
                RotationAxis.X => _rotationValue + new Vector3(newAngle, 0, 0),
                RotationAxis.Y => _rotationValue + new Vector3(0, newAngle, 0),
                RotationAxis.Z => _rotationValue + new Vector3(0, 0, newAngle)
            };

            this.transform.DOLocalRotate(_rotationValue, Time.fixedDeltaTime);
            {
                
            }
        }
    }
}
