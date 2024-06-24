using UnityEngine;

namespace Dimasyechka.Code.WeaponrySystem.Projectiles
{
    public class ProjectileEffects : MonoBehaviour
    {
        [SerializeField]
        private ProjectileCollisions _projectileCollisions;


        [SerializeField]
        private ParticleSystem _onDisposeEffectPrefab;

        [SerializeField]
        private float _disposeEffectLifeTime = 1f;


        private void Awake()
        {
            _projectileCollisions.onProjectileDispose += OnProjectileDispose;
        }

        private void OnDestroy()
        {
            _projectileCollisions.onProjectileDispose -= OnProjectileDispose;
        }


        protected virtual void OnProjectileDispose()
        {
            ParticleSystem effect = Instantiate(_onDisposeEffectPrefab, transform.position, Quaternion.identity);

            effect.Play();

            Destroy(effect.gameObject, _disposeEffectLifeTime);
        }
    }
}
