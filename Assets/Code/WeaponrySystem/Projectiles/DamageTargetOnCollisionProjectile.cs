using Dimasyechka.Code.HealthSystem;
using UnityEngine;

namespace Dimasyechka.Code.WeaponrySystem.Projectiles
{
    public class DamageTargetOnCollisionProjectile : MonoBehaviour
    {
        [SerializeField]
        private ProjectileCore _projectileCore;

        [SerializeField]
        private ProjectileCollisions _projectileCollisions;


        private void Awake()
        {
            _projectileCollisions.onTargetCollision += OnTargetCollision;
        }

        private void OnDestroy()
        {
            _projectileCollisions.onTargetCollision -= OnTargetCollision;
        }


        private void OnTargetCollision(GameObject go, IDamageable damageable)
        {
            damageable.DamageInstance(_projectileCore.RuntimeConfig.Damage);
        }
    }
}
