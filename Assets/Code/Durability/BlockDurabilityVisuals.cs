using System.Collections.Generic;
using Dimasyechka.Code.HealthSystem;
using Dimasyechka.Lubribrary.RxMV.UniRx.Attributes;
using Dimasyechka.Lubribrary.RxMV.UniRx.RxLink;
using UniRx;
using UnityEngine;

namespace Dimasyechka.Code.Durability
{
    public class BlockDurabilityVisuals : MonoBehaviour, IRxLinkable
    {
        [SerializeField]
        private List<Sprite> _durabilitySprites = new List<Sprite>();

        [SerializeField]
        private HealthComponent _health;


        [RxAdaptableProperty]
        public ReactiveProperty<Sprite> DurabilitySprite = new ReactiveProperty<Sprite>();

        [RxAdaptableProperty]
        public ReactiveProperty<Color> Transparency = new ReactiveProperty<Color>();


        public int Durability => (int)_health.Health.Value;


        public void Init(int durability)
        {
            _health.SetHealthAndMaxHealth(durability);
            Transparency.Value = new Color(0, 0, 0, 0);
        }

        public void DecreaseDurability(int damage)
        {
            _health.DamageInstance(damage);
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            Transparency.Value = new Color(1, 1, 1, 1);

            /* 
             * 0 - 10
             * 1 2 3 4 5
             * 4 / 5 = 0.8 
             * 1 - 0.8 = 0.2 => 2 из [0:10) 
             */
            float ratio = 1 - _health.HealthRatio.Value;

            int index = Mathf.FloorToInt(ratio * _durabilitySprites.Count);

            index = Mathf.Clamp(index, 0, _durabilitySprites.Count - 1);

            DurabilitySprite.Value = _durabilitySprites[index];
        }
    }
}
