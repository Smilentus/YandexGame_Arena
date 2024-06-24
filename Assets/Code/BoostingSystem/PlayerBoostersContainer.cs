using System.Collections.Generic;
using UnityEngine;

namespace Dimasyechka.Code.BoostingSystem
{
    public class PlayerBoostersContainer : MonoBehaviour
    {
        private List<PlayerBooster> _availableBoosters = new List<PlayerBooster>();


        private PlayerBooster[] _usedBoosters = new PlayerBooster[5];
        public PlayerBooster[] UsedBoosters => _usedBoosters;
    }
}