using UnityEngine;

namespace Dimasyechka.Code.WeaponrySystem.Projectiles
{
    public class ProjectileMover : MonoBehaviour
    {
        [SerializeField]
        private ProjectileCore _projectileCore;

        [SerializeField]
        private Rigidbody _rigidbody;


        private void FixedUpdate()
        {
            MoveProjectile();
        }


        protected virtual void MoveProjectile()
        {
            _rigidbody.AddForce(
                _rigidbody.transform.forward * _projectileCore.RuntimeConfig.MovementSpeed * Time.fixedDeltaTime,
                ForceMode.VelocityChange);
        }
    }
}
