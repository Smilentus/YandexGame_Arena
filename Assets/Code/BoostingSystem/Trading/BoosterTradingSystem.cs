using Zenject;

namespace Dimasyechka.Code.BoostingSystem.Trading
{
    public class BoosterTradingSystem
    {
        private BoosterTradingScriptableInstaller _boosterTradingRules;
        public BoosterTradingScriptableInstaller BoosterTradingRules => _boosterTradingRules;


        [Inject]
        public void Construct(BoosterTradingScriptableInstaller boosterTradingRules)
        {
            _boosterTradingRules = boosterTradingRules;
        }
    }
}
