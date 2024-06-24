using UnityEngine;

namespace Dimasyechka.Code.PlayerSystem
{
    public class RuntimePlayerObject : MonoBehaviour
    {
        private RuntimePlayerStats _runtimePlayerStats = new RuntimePlayerStats();
        public RuntimePlayerStats RuntimePlayerStats => _runtimePlayerStats;


        public void LoadPlayerStats(RuntimePlayerStats loadablePlayerStats)
        {
            _runtimePlayerStats = loadablePlayerStats;
        }
    }
}