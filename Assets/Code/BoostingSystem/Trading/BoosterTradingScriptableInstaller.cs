using System;
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


        private void OnEnable()
        {
            SetEditorNames();
        }

        private void OnValidate()
        {
            SetEditorNames();
        }


        private void SetEditorNames()
        {
            foreach (BoosterTradingRules rule in TradingRules)
            {
                rule.InspectorName = $"{rule.InputBooster.Guid} x{rule.InputAmount} -> {rule.OutputBooster.Guid} x{rule.OutputAmount}";
            }
        }


        public override void InstallBindings()
        {
            Container.Bind<BoosterTradingScriptableInstaller>().FromInstance(this).AsSingle();
        }
    }
}
