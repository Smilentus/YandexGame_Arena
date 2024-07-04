namespace Dimasyechka.Code.PlayerSystem
{
    public class RuntimePlayerStats
    {
        public string PlayerPrefix;

        public double MaxHealth
        {
            get
            {
                if (BodyPower <= 10)
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
                if (HandsPower <= 0)
                {
                    return 1;
                }

                return HandsPower;
            }
        }

        public double Fatigue;
        public double MaxFatigue;

        public double TotalPower => BodyPower + HandsPower + LegsPower;

        public double BodyPower;
        public double HandsPower;
        public double LegsPower;


        public uint Coins;


        public bool HasPickaxe;


        public RuntimePlayerStats()
        {
            PlayerPrefix = "Неизвестный";

            Coins = 100000;

            Fatigue = 0;
            MaxFatigue = 100;

            BodyPower = 0;
            HandsPower = 0;
            LegsPower = 0;

            HasPickaxe = false;
        }
    }
}
