using System;
using System.Collections.Generic;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    public class PlayerBoostersContainer
    {
        public event Action onAvailableBoostersChanged;
        public event Action onUsedBoostersChanged;


        private List<ObtainedBooster> _availableBoosters = new List<ObtainedBooster>();
        public IEnumerable<ObtainedBooster> AvailableBoosters => _availableBoosters;


        private PlayerBooster[] _usedBoosters = new PlayerBooster[5];
        public PlayerBooster[] UsedBoosters => _usedBoosters;


        private PlayerBoostersWarehouse _boostersWarehouse;

        [Inject]
        public void Construct(PlayerBoostersWarehouse warehouse)
        {
            _boostersWarehouse = warehouse;
        }


        public void AddBooster(PlayerBooster booster, bool autoEquip = true)
        {
            AddBooster(booster.Guid, autoEquip);
        }

        public void AddBooster(string boosterGuid, bool autoEquip = true)
        {
            _availableBoosters.Add(new ObtainedBooster()
            {
                Guid = boosterGuid
            });

            if (autoEquip)
                TryEquipBooster(boosterGuid);

            onAvailableBoostersChanged?.Invoke();
        }

        public void RemoveBooster(string boosterGuid)
        {
            ObtainedBooster booster = _availableBoosters.Find(x => x.Guid == boosterGuid);

            if (booster == null)
            {
                UnityEngine.Debug.LogError($"[RemoveBooster] => Пытаемся удалить несуществующий бустер из доступных!");
                return;
            }

            _availableBoosters.Remove(booster);

            onAvailableBoostersChanged?.Invoke();
        }


        public double GetMultiplicationFactor()
        {
            double mul = 1;
            for (int i = 0; i < _usedBoosters.Length; i++)
            {
                if (_usedBoosters[i] != null)
                {
                    mul += _usedBoosters[i].Value;
                }
            }
            return mul;
        }


        public bool TryEquipBooster(string boosterGuid)
        {
            for (int i = 0; i < _usedBoosters.Length; i++)
            {
                if (_usedBoosters[i] == null)
                {
                    _usedBoosters[i] = _boostersWarehouse.GetBoosterByGuid(boosterGuid);

                    if (_usedBoosters[i] == null)
                    {
                        UnityEngine.Debug.LogError($"[TryUseBooster] => не установлен бустер, который должен быть установлен!");
                        return false;
                    }

                    RemoveBooster(boosterGuid);

                    onUsedBoostersChanged?.Invoke();
                    onAvailableBoostersChanged?.Invoke();

                    return true;
                }
            }

            return false;
        }

        public void UnEquipBooster(int slotIndex)
        {
            if (slotIndex < 0 && slotIndex >= _usedBoosters.Length) return;

            if (_usedBoosters[slotIndex] != null)
            {
                AddBooster(_usedBoosters[slotIndex].Guid, false);

                _usedBoosters[slotIndex] = null;

                onUsedBoostersChanged?.Invoke();
                onAvailableBoostersChanged?.Invoke();
            }
        }


        public void LoadBoosters(List<string> avaiableBoosters, List<string> usedBoosters)
        {
            // TODO: ...
        }

        // TODO: Save Boosters
    }


    [System.Serializable]
    public class ObtainedBooster
    {
        public string Guid;
    }
}