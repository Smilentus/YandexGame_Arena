using Dimasyechka.Code.PlayerSystem;
using System.Collections.Generic;
using Zenject;

namespace Dimasyechka
{
    public class FortuneWheelPrizeGiver : IInitializable
    {
        private List<BaseFortuneWheelPrizeHandler> _handlers = new List<BaseFortuneWheelPrizeHandler>();


        private RuntimePlayerObject _runtimePlayerObject;

        [Inject]
        public void Construct(RuntimePlayerObject runtimePlayerObject)
        {
            _runtimePlayerObject = runtimePlayerObject;
        }


        public void Initialize()
        {
            _handlers.Add(new CoinsPrizeHandler("prize_coins"));
            _handlers.Add(new BodyPrizeHandler("prize_body"));
            _handlers.Add(new HandsPrizeHandler("prize_hands"));
            _handlers.Add(new LegsPrizeHandler("prize_legs"));

            foreach (var handler in _handlers)
            {
                handler.Init(_runtimePlayerObject);
            }
        }


        public void TryGizePrize(string prizeGuid, double value)
        {
            for (int i = 0; i < _handlers.Count; i++)
            {
                _handlers[i].TryGivePrize(prizeGuid, value);
            }
        }
    }


    public abstract class BaseFortuneWheelPrizeHandler
    {
        protected readonly string _prizeGuid;


        protected RuntimePlayerObject _runtimePlayerObject;
        
        public void Init(RuntimePlayerObject runtimePlayerObject)
        {
            _runtimePlayerObject = runtimePlayerObject;
        }


        public BaseFortuneWheelPrizeHandler(string prizeGuid)
        {
            _prizeGuid = prizeGuid;
        }


        public void TryGivePrize(string prizeGuid, double value)
        {
            if (_prizeGuid.Equals(prizeGuid))
            {
                OnGivePrize(value);
            }
        }

        protected abstract void OnGivePrize(double value);
    }


    public class CoinsPrizeHandler : BaseFortuneWheelPrizeHandler
    {
        public CoinsPrizeHandler(string prizeGuid) : base(prizeGuid) { }

        protected override void OnGivePrize(double value)
        {
            _runtimePlayerObject.RuntimePlayerStats.Coins += (uint)value;
        }
    }

    public class BodyPrizeHandler : BaseFortuneWheelPrizeHandler
    {
        public BodyPrizeHandler(string prizeGuid) : base(prizeGuid) { }

        protected override void OnGivePrize(double value)
        {
            _runtimePlayerObject.RuntimePlayerStats.BodyPower += value;
        }
    }

    public class HandsPrizeHandler : BaseFortuneWheelPrizeHandler
    {
        public HandsPrizeHandler(string prizeGuid) : base(prizeGuid) { }

        protected override void OnGivePrize(double value)
        {
            _runtimePlayerObject.RuntimePlayerStats.HandsPower += value;
        }
    }

    public class LegsPrizeHandler : BaseFortuneWheelPrizeHandler
    {
        public LegsPrizeHandler(string prizeGuid) : base(prizeGuid) { }

        protected override void OnGivePrize(double value)
        {
            _runtimePlayerObject.RuntimePlayerStats.LegsPower += value;
        }
    }
}