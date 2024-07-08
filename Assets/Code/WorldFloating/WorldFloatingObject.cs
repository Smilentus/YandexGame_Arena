using UnityEngine;

namespace Dimasyechka.Code.WorldFloating
{
    public class WorldFloatingObject : MonoBehaviour
    {
        [SerializeField]
        private float _moveSpeed = 3f;

        [SerializeField]
        private float _lifeTimeSeconds = 2f;


        private void Awake()
        {
            Destroy(this.gameObject, _lifeTimeSeconds);
        }

        private void FixedUpdate()
        {
            this.transform.position += Vector3.up * (_moveSpeed * Time.fixedDeltaTime);
        }
    }
}
