using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.FortuneWheel
{
    public class FortuneWheel : IInitializable, IFixedTickable
    {
        private TimeSpan _defaultTimeBetweenSpins = TimeSpan.FromMinutes(3f);
        private int _minimumSlots = 6;
        private int _maximumSlots = 10;


        private List<FortuneWheelSlotData> _prizes = new List<FortuneWheelSlotData>();
        public List<FortuneWheelSlotData> Prizes => _prizes;


        public ReactiveProperty<TimeSpan> TimeFromLastSpin = new ReactiveProperty<TimeSpan>();
        public ReactiveProperty<bool> IsSpinAvailable = new ReactiveProperty<bool>();


        private int _currentSlots;
        public int CurrentSlots => _currentSlots;


        private int _fortuneWheelLevel = 0;


        private PrizesWarehouse _warehouse;
        private FortuneWheelPrizeGiver _prizeGiver;

        [Inject]
        public void Construct(
            PrizesWarehouse warehouse,
            FortuneWheelPrizeGiver prizeGiver)
        {
            _warehouse = warehouse;
            _prizeGiver = prizeGiver;
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
            SetNewFortuneWheel();
        }

        private void Save()
        {

        }


        private void SaveSpinTime()
        {

        }


        private void SetNewFortuneWheel()
        {
            TimeFromLastSpin.Value = TimeSpan.FromSeconds(0);
            IsSpinAvailable.Value = true;

            _fortuneWheelLevel++;

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
                            Prize = prize,
                            SlotIndex = i,
                            Value = UnityEngine.Random.Range(prize.MinValue * _fortuneWheelLevel, prize.MaxValue * _fortuneWheelLevel)
                        };

                        _prizes.Add(slotData);
                        break;
                    }
                }
            }

            Debug.Log("Fortune Wheel Generated New Prizes");

            Save();
        }


        public void GivePrize(string prizeGuid, double value)
        {
            _prizeGiver.TryGizePrize(prizeGuid, value);
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
        public Prize Prize;
        public int Value;
    }
}
