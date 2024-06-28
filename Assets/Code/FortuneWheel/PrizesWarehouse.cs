using Dimasyechka.Code.ShopSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "PrizesWarehouse", menuName = "FortuneWheel/New PrizesWarehouse")]
    public class PrizesWarehouse : ScriptableObjectInstaller
    {
        [field: SerializeField]
        public List<Prize> AvailablePrizes = new List<Prize>();


        private List<RandomizableArea> _randomPrizesWithWeights = new List<RandomizableArea>();
        public List<RandomizableArea> RandomPrizesWithWeights => _randomPrizesWithWeights;


        public override void InstallBindings()
        {
            Container.Bind<PrizesWarehouse>().FromInstance(this).AsSingle();
        }


        public Prize GetPrizeByGuid(string guid)
        {
            return AvailablePrizes.Find(x => x.Guid == guid);
        }


        public float GetMaxWeight()
        {
            if (_randomPrizesWithWeights == null || _randomPrizesWithWeights.Count == 0)
            {
                SetRandomAreas();
            }

            if (_randomPrizesWithWeights.Count == 0) return 0;

            return _randomPrizesWithWeights[^1].Max;
        }

        private void SetRandomAreas()
        {
            _randomPrizesWithWeights = GetRandomAreas();
        }

        private List<RandomizableArea> GetRandomAreas()
        {
            float startArea = 0;

            List<RandomizableArea> areas = new List<RandomizableArea>();
            List<Prize> prizes = AvailablePrizes.OrderBy(x => x.RandomWeight).ToList();

            for (int i = 0; i < prizes.Count; i++)
            {
                RandomizableArea newArea = new RandomizableArea()
                {
                    Guid = prizes[i].Guid
                };

                if (i == 0)
                {
                    newArea.Min = startArea;
                }
                else
                {
                    newArea.Min = areas[i - 1].Max;
                }

                newArea.Max = newArea.Min + prizes[i].RandomWeight;

                areas.Add(newArea);
            }

            return areas;
        }
    }
}
