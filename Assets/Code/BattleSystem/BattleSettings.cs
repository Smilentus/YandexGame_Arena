using System;

namespace Dimasyechka
{
    [System.Serializable]
    public class BattleSettings
    {
        public BattleCharacterProfile[] EnemyProfiles;
        public float BattleTimeSeconds = 180;
    }
}
