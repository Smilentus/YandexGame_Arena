using UnityEngine;

namespace Dimasyechka.Code.BoostingSystem.Trading
{
    [System.Serializable]
    public class BoosterTradingRules
    {
        public string InspectorName;

        [Header("Input")]
        public PlayerBooster InputBooster;
        public int InputAmount;

        [Header("Output")]
        public PlayerBooster OutputBooster;
        public int OutputAmount;
    }
}
