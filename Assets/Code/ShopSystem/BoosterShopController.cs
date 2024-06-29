using Dimasyechka.Code.BoostingSystem;
using Dimasyechka.Code.PlayerSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.ShopSystem
{
    public class BoosterShopController : MonoBehaviour
    {
        [field: SerializeField]
        public List<RandomizableBooster> SellableBoosters = new List<RandomizableBooster>();

        [field: SerializeField]
        public uint BuyPrice = 0;


        private PlayerBoostersContainer _playerBoostersContainer;
        private RuntimePlayerObject _runtimePlayerObject;

        [Inject]
        public void Construct(
            PlayerBoostersContainer playerBoostersContainer,
            RuntimePlayerObject runtimePlayerObject)
        {
            _playerBoostersContainer = playerBoostersContainer;
            _runtimePlayerObject = runtimePlayerObject;
        }


        public bool TryBuyRandomBooster()
        {
            if (_runtimePlayerObject.RuntimePlayerStats.Coins < BuyPrice) { return false; }

            _runtimePlayerObject.RuntimePlayerStats.Coins -= BuyPrice;

            float random = Random.Range(0, 101);

            List<RandomizableArea> tempAreas = GetRandomAreas();

            Debug.Log($"Random booster chance: {random}");

            foreach (RandomizableArea rndArea in tempAreas)
            {
                if (rndArea.IsInArea(random))
                {
                    Debug.Log($"Obtained booster: {rndArea.Guid}");
                    _playerBoostersContainer.AddBooster(rndArea.Guid);
                    break;
                }
            }

            return true;
        }


        public List<RandomizableArea> GetRandomAreas()
        {
            float startArea = 0;

            List<RandomizableArea> areas = new List<RandomizableArea>();
            List<RandomizableBooster> boosters = SellableBoosters.OrderBy(x => x.ObtainChance).ToList();

            for (int i = 0; i < boosters.Count; i++)
            {
                RandomizableArea newArea = new RandomizableArea()
                {
                    Guid = boosters[i].Booster.Guid
                };

                if (i == 0)
                {
                    newArea.Min = startArea;
                }
                else
                {
                    newArea.Min = areas[i - 1].Max;
                }

                newArea.Max = newArea.Min + boosters[i].ObtainChance;

                areas.Add(newArea);
            }

            return areas;
        }
    }

    public class RandomizableArea
    {
        public string Guid;

        public float Min;
        public float Max;

        public bool IsInArea(float value) => value >= Min && value <= Max;
    }

    [System.Serializable]
    public class RandomizableBooster
    {
        [Range(0, 100)]
        public float ObtainChance;

        public PlayerBooster Booster;
    }
}
