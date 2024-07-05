using System.Collections.Generic;
using Dimasyechka.Code.Achievements.RewardHandlers;
using Dimasyechka.Code.BoostingSystem;
using Zenject;

namespace Dimasyechka.Code.PlayerSystem
{
    public class RuntimePlayerUpgrader
    {
        private PlayerBoostersContainer _playerBoostersContainer;
        private RuntimePlayerObject _runtimePlayerObject;

        [Inject]
        public void Construct(
            PlayerBoostersContainer playerBoostersContainer,
            RuntimePlayerObject runtimePlayerObject)
        {
            _runtimePlayerObject = runtimePlayerObject;
            _playerBoostersContainer = playerBoostersContainer;
        }


        public void SetupRewardHandler(ref RewardHandler rewardHandler)
        {
            rewardHandler.Setup(new List<object>()
            {
                _runtimePlayerObject,
                _playerBoostersContainer
            });
        }


        public void AddCoins(uint value) => _runtimePlayerObject.RuntimePlayerStats.Coins += value;


        public void UpgradeBodyPowerRaw(double value) =>
            _runtimePlayerObject.RuntimePlayerStats.BodyPower += value;
        public void UpgradeHandsPowerRaw(double value) =>
            _runtimePlayerObject.RuntimePlayerStats.HandsPower += value;
        public void UpgradeLegsPowerRaw(double value) =>
            _runtimePlayerObject.RuntimePlayerStats.LegsPower += value;


        public void UpgradeBodyPowerWithMul(double value) =>
            _runtimePlayerObject.RuntimePlayerStats.BodyPower += _playerBoostersContainer.GetMultiplicationFactor();
        public void UpgradeHandsPowerWithMul(double value) =>
            _runtimePlayerObject.RuntimePlayerStats.HandsPower += _playerBoostersContainer.GetMultiplicationFactor();
        public void UpgradeLegsPowerWithMul(double value) =>
            _runtimePlayerObject.RuntimePlayerStats.LegsPower += _playerBoostersContainer.GetMultiplicationFactor();

    }
}
