using UnityEngine;

namespace Dimasyechka.Code.WeaponrySystem.Projectiles
{
    public class ProjectileLifeTimer : MonoBehaviour
    {
        [SerializeField]
        private ProjectileCore _projectileCore;


        private float _passedTime = 0;

        private void FixedUpdate()
        {
            CalculateLifeTime();
        }


        private void CalculateLifeTime()
        {
            if (!this.gameObject.activeSelf) return;

            if (_passedTime < _projectileCore.RuntimeConfig.ProjectileLifeTime)
            {
                _passedTime += Time.fixedDeltaTime;
            }
            else
            {
                OnLifeTimePassed();
            }
        }


        protected virtual void OnLifeTimePassed()
        {
            Destroy(_projectileCore.gameObject);
        }
    }
}
