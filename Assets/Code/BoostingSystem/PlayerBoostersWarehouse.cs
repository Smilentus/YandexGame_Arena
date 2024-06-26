using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BoostingSystem
{
    [CreateAssetMenu(fileName = "BoostersWarehouse", menuName = "PlayerBoosters/BoostersWarehouse")]
    public class PlayerBoostersWarehouse : ScriptableObjectInstaller
    {
        [field: SerializeField]
        public List<PlayerBooster> AllBoosters = new List<PlayerBooster>();


        public PlayerBooster GetBoosterByGuid(string boosterGuid)
        {
            return AllBoosters.Find(x => x.Guid == boosterGuid);
        }


        public override void InstallBindings()
        {
            Container.Bind<PlayerBoostersWarehouse>().FromInstance(this).AsSingle();
        }
    }
}
