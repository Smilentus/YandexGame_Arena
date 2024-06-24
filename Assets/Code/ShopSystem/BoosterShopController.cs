using Dimasyechka.Code.BoostingSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Dimasyechka.Code.ShopSystem
{
    public class BoosterShopController : MonoBehaviour
    {
        [field: SerializeField]
        public List<RandomizableBooster> SellableBoosters = new List<RandomizableBooster>();

        [field: SerializeField]
        public double BuyPrice = 0;
        

        public void BuyRandomBooster()
        {
            float random = Random.Range(0, 101);
            
            // TODO: ...
        }
    }

    [System.Serializable]
    public class RandomizableBooster
    {
        [Range(0, 100)]
        public float ObtainChance;

        public PlayerBooster Booster;
    }
}
