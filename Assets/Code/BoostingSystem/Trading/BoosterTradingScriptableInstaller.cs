using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem.Trading
{
    [CreateAssetMenu(fileName = "BoosterTradingScriptableInstaller", menuName = "BoosterSystem/Trading/New TradingInstaller")]
    public class BoosterTradingScriptableInstaller : ScriptableObjectInstaller
    {
        [field: SerializeField]
        public List<BoosterTradingRules> TradingRules = new List<BoosterTradingRules>();


        public override void InstallBindings()
        {
            Container.Bind<BoosterTradingScriptableInstaller>().FromInstance(this).AsSingle();
        }
    }
}
