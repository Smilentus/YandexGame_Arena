using Dimasyechka.Code.PlayerSystem;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public class EnemyLookAtPlayerBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float _rotationSpeed = 3f;

        [SerializeField]
        private Transform _headAiming;


        private PlayerObjectTag _playerObjectTag;

        [Inject]
        public void Construct(PlayerObjectTag playerObjectTag)
        {
            _playerObjectTag = playerObjectTag;
        }


        private void FixedUpdate()
        {
            AimToPlayer();
            RotateToPlayer();
        }


        private void AimToPlayer()
        {
            Vector3 target = transform.position + (transform.forward * 2);

            if (_playerObjectTag != null)
            {
                target = new Vector3(
                    _playerObjectTag.transform.position.x,
                    _playerObjectTag.transform.position.y < 2 ? 2 : _playerObjectTag.transform.position.y,
                    _playerObjectTag.transform.position.z
                );
            }

            _headAiming.position = target;
        }

        private void RotateToPlayer()
        {
            Quaternion lookRotation = Quaternion.LookRotation(
                (_playerObjectTag.transform.position - transform.position).normalized,
                Vector3.up
            );

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * _rotationSpeed);
        }
    }
}
