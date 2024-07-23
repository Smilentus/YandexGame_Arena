using System;
using UnityEngine;

namespace Dimasyechka.Code.PlayerSystem
{
    public class RuntimePlayerObject : MonoBehaviour
    {
        public event Action onPlayerStatsLoaded;


        private RuntimePlayerStats _runtimePlayerStats = new RuntimePlayerStats();
        public RuntimePlayerStats RuntimePlayerStats => _runtimePlayerStats;


        private void Awake()
        {
            // Temporal load
            RuntimePlayerStats stats = new RuntimePlayerStats();


        }

        private void OnDestroy()
        {
            // Temporal save
        }


        public void LoadPlayerStats(RuntimePlayerStats loadablePlayerStats)
        {
            _runtimePlayerStats = loadablePlayerStats;

            onPlayerStatsLoaded?.Invoke();
        }
    }
}