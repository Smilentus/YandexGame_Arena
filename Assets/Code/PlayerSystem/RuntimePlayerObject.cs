using System;
using UnityEngine;

namespace Dimasyechka.Code.PlayerSystem
{
    public class RuntimePlayerObject : MonoBehaviour
    {
        public event Action onPlayerStatsLoaded;


        private RuntimePlayerStats _runtimePlayerStats = new RuntimePlayerStats();
        public RuntimePlayerStats RuntimePlayerStats => _runtimePlayerStats;


        public void LoadPlayerStats(RuntimePlayerStats loadablePlayerStats)
        {
            _runtimePlayerStats = loadablePlayerStats;

            onPlayerStatsLoaded?.Invoke();
        }


        private void Update()
        {
            if (Input.GetKey(KeyCode.G))
            {
                _runtimePlayerStats.BodyPower += 1000;
                _runtimePlayerStats.LegsPower += 1000;
                _runtimePlayerStats.HandsPower += 1000;

                _runtimePlayerStats.Coins += 1000;
            }
        }
    }
}