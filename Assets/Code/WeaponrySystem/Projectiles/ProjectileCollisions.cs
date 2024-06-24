using System;
using Dimasyechka.Code.HealthSystem;
using UnityEngine;

namespace Dimasyechka.Code.WeaponrySystem.Projectiles
{
    public class ProjectileCollisions : MonoBehaviour
    {
        public event Action onProjectileDispose;
        public event Action<GameObject> onAnyCollision;
        public event Action<GameObject> onOtherCollision;
        public event Action<GameObject, IDamageable> onTargetCollision;


        public enum ProjectileDisposeType
        {
            AnyCollision,
            TargetCollision,
            OtherCollision
        }


        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private bool _includeTriggers;


        [SerializeField]
        private ProjectileDisposeType _disposeType = ProjectileDisposeType.AnyCollision;
        public ProjectileDisposeType DisposeType => _disposeType;


        private bool _isProjectileDisposed;


        private void OnCollisionEnter(Collision collision)
        {
            ProcessCollisionsOrTriggers(collision.gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            ProcessCollisionsOrTriggers(collider.gameObject);
        }


        private void ProcessCollisionsOrTriggers(GameObject processableObject)
        {
            if (_isProjectileDisposed) return;


            if (processableObject.TryGetComponent(out IDamageable damageable))
            {
                onTargetCollision?.Invoke(processableObject, damageable);

                if (_disposeType == ProjectileDisposeType.TargetCollision)
                {
                    DisposeProjectile();
                }
            }
            else
            {
                onOtherCollision?.Invoke(processableObject);

                if (_disposeType == ProjectileDisposeType.OtherCollision)
                {
                    DisposeProjectile();
                }
            }

            onAnyCollision?.Invoke(processableObject);

            if (_disposeType == ProjectileDisposeType.AnyCollision)
            {
                DisposeProjectile();
            }
        }


        private void DisposeProjectile()
        {
            if (_isProjectileDisposed) return;

            onProjectileDispose?.Invoke();

            Destroy(gameObject);

            _isProjectileDisposed = true;
        }
    }
}
