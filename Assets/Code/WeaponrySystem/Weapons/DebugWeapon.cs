using Dimasyechka.Code.WeaponrySystem.Projectiles;
using UnityEngine;

namespace Dimasyechka.Code.WeaponrySystem.Weapons
{
    public class DebugWeapon : MonoBehaviour
    {
        [SerializeField]
        private ProjectileCore _proj;

        [SerializeField]
        private Transform _projSpawnPoint;


        private float _timer;


        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;

            if (_timer >= 0.5f)
            {
                _timer = 0;

                SpawnProjectile();
            }
        }


        private void SpawnProjectile()
        {
            Instantiate(_proj, _projSpawnPoint.position, Quaternion.identity);
        }
    }
}
