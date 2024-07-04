using UnityEngine;

namespace Dimasyechka
{
    [CreateAssetMenu(fileName = "BattleCharacterProfile", menuName = "BattleSystem/New BattleCharacterProfile")]
    public class BattleCharacterProfile : ScriptableObject
    {
        [field: SerializeField]
        public RuntimeBattleCharacter CharacterPrefab;

        [field: SerializeField]
        public string CharacterName;

        [field: SerializeField]
        public double Health;

        [field: SerializeField]
        public double Damage;
    }
}
