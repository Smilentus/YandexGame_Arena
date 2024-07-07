namespace Dimasyechka.Code.HealthSystem
{
    public interface IDamageable
    {
        public bool IsAlive();
        public void DamageInstance(double damage);
    }
}
