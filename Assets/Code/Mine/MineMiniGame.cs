using UnityEngine;

namespace Dimasyechka.Code.Mine
{
    public class MineMiniGame
    {
        public Mine Mine;

        private int _randomOres;
        public int RandomOres => _randomOres;


        public MineMiniGame(Mine mine)
        {
            Mine = mine;

            _randomOres = Random.Range(5, 10);
        }
    }
}
