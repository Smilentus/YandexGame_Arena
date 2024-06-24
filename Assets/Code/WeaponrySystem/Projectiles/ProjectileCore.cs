using UnityEngine;

namespace Dimasyechka.Code.WeaponrySystem.Projectiles
{
    public class ProjectileCore : MonoBehaviour
    {
        [SerializeField]
        private RuntimeProjectileConfig _runtimeConfig;
        public RuntimeProjectileConfig RuntimeConfig => _runtimeConfig;


        public void SetupConfig(RuntimeProjectileConfig config)
        {
            _runtimeConfig = config;
        }
    }
}
