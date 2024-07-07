namespace Dimasyechka.Code.PlayerSystem
{
    public class RuntimePlayerStats
    {
        public string PlayerPrefix = "Неизвестный";

        public double MaxHealth
        {
            get
            {
                if (BodyPower < 10)
                {
                    return 10;
                }

                return BodyPower;
            }
        }

        public double Damage
        {
            get
            {
                if (HandsPower < 1)
                {
                    return 1;
                }

                return HandsPower;
            }
        }

        public double Fatigue = 0;
        public double MaxFatigue = 100;

        public double TotalPower => BodyPower + HandsPower + LegsPower;

        public double BodyPower = 0;
        public double HandsPower = 0;
        public double LegsPower = 0;


        public uint Coins = 100000;


        public bool HasPickaxe = false;
    }
}
