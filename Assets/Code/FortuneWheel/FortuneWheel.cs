using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class FortuneWheel : IInitializable, IFixedTickable
    {
        private TimeSpan _defaultTimeBetweenSpins = TimeSpan.FromMinutes(3);
        private int _minimumSlots = 6;
        private int _maximumSlots = 12;


        private List<FortuneWheelSlotData> _prizes = new List<FortuneWheelSlotData>();
        public List<FortuneWheelSlotData> Prizes => _prizes;


        public ReactiveProperty<TimeSpan> TimeFromLastSpin = new ReactiveProperty<TimeSpan>();
        public ReactiveProperty<bool> IsSpinAvailable = new ReactiveProperty<bool>();


        private int _currentSlots;
        public int CurrentSlots => _currentSlots;


        private PrizesWarehouse _warehouse;

        [Inject]
        public void Construct(PrizesWarehouse warehouse)
        {
            _warehouse = warehouse;
        }


        public void Initialize()
        {
            Load();
        }

        public void FixedTick()
        {
            if (TimeFromLastSpin.Value.TotalSeconds > 0)
            {
                TimeFromLastSpin.Value -= (TimeSpan.FromSeconds((float)Time.fixedDeltaTime));

                if (TimeFromLastSpin.Value.TotalSeconds <= 0)
                {
                    SetNewFortuneWheel();
                }
            }
        }


        private void Load()
        {
            Debug.Log("Load Fortune Wheel");

            SetNewFortuneWheel();
        }

        private void Save()
        {
            Debug.Log("Save Fortune Wheel");
        }


        private void SaveSpinTime()
        {
            Debug.Log("Save Spin Time");
        }


        private void SetNewFortuneWheel()
        {
            TimeFromLastSpin.Value = TimeSpan.FromSeconds(0);
            IsSpinAvailable.Value = true;

            GeneratePrizes();
        }


        public void GeneratePrizes()
        {
            _prizes.Clear();

            _currentSlots = UnityEngine.Random.Range(_minimumSlots, _maximumSlots);

            for (int i = 0; i < _currentSlots; i++)
            {
                float randomPrizeWeight = UnityEngine.Random.Range(0, _warehouse.GetMaxWeight());

                foreach (var randomPrizeArea in _warehouse.RandomPrizesWithWeights)
                {
                    if (randomPrizeArea.IsInArea(randomPrizeWeight))
                    {
                        Prize prize = _warehouse.GetPrizeByGuid(randomPrizeArea.Guid);

                        FortuneWheelSlotData slotData = new FortuneWheelSlotData()
                        {
                            PrizeGuid = randomPrizeArea.Guid,
                            SlotIndex = i,
                            Value = UnityEngine.Random.Range(prize.MinValue, prize.MaxValue)
                        };

                        _prizes.Add(slotData);
                        break;
                    }
                }
            }

            Debug.Log("Fortune Wheel Generated New Prizes");

            Save();
        }


        public void Spin()
        {
            if (!IsSpinAvailable.Value) return;

            IsSpinAvailable.Value = false;
            TimeFromLastSpin.Value = _defaultTimeBetweenSpins;

            SaveSpinTime();
        }
    }


    [System.Serializable]
    public class FortuneWheelSlotData
    {
        public int SlotIndex;
        public string PrizeGuid;
        public int Value;
    }
}
