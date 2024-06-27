namespace Dimasyechka.Code.PlayerSystem
{
    public class RuntimePlayerStats
    {
        public double MaxHealth;

        public double Damage;
        public double Dodge;
        public double Accuracy;

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
            MaxHealth = 100;
            Damage = 1;
            Dodge = 0;
            Accuracy = 80;

            Fatigue = 0;
            MaxFatigue = 100;

            BodyPower = 0;
            HandsPower = 0;
            LegsPower = 0;

            HasPickaxe = false;
        }
    }
}
