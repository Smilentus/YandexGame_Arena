namespace Dimasyechka.Code.PlayerSystem
{
    public class RuntimePlayerStats
    {
        public double MaxHealth;

        public double Damage;
        public double Dodge;
        public double Accuracy;


        public RuntimePlayerStats()
        {
            MaxHealth = 100;
            Accuracy = 80;
            Damage = 1;
            Dodge = 0;
        }
    }
}
