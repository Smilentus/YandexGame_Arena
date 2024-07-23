namespace Dimasyechka.Code.BattleSystem
{
    [System.Serializable]
    public class BattleSettings
    {
        public BattleCharacterProfile EnemyProfile;
        public float BattleTimeSeconds = 180;
        public uint WinCoins = 100;
        public int EnemiesInBattle = 1;
    }
}
