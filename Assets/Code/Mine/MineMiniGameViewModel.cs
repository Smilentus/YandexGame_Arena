using Dimasyechka.Code.PlayerSystem;
using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Code.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG.Example;
using Zenject;

namespace Dimasyechka.Code.Mine
{
    public class MineMiniGameViewModel : BaseShopViewModel<MineMiniGame>
    {
        [SerializeField]
        private MineableRewardViewModel _mineableRewardViewModel;


        [SerializeField]
        private MineableItemViewModel _mineableItemViewModelPrefab;

        [SerializeField]
        private List<Transform> _spawnPoints = new List<Transform>();

        private int _generatedOres = 0;
        private int _clickedOres = 0;


        private Dictionary<string, int> _generatedDictionary = new Dictionary<string, int>();
        private List<MineableItemViewModel> _generatedViewModels = new List<MineableItemViewModel>();

        private Mine _mineOrigin;

        private MineableItemViewModelFactory _factory;
        private RuntimePlayerUpgrader _playerUpgrader;

        [Inject]
        public void Construct(
            MineableItemViewModelFactory factory,
            RuntimePlayerUpgrader playerUpgrader)
        {
            _playerUpgrader = playerUpgrader;

            _factory = factory;
        }


        public override void OnHide()
        {
            StopAllCoroutines();
        }

        private void DrawUI()
        {
            _mineableRewardViewModel.gameObject.SetActive(false);


            List<Transform> tempSpawnPoints = GetRandomPointsDistinct();

            for (int i = 0; i < tempSpawnPoints.Count; i++)
            {
                MineableItemViewModel viewModel =
                    _factory.InstantiateForComponent(_mineableItemViewModelPrefab.gameObject, tempSpawnPoints[i]);

                MineableItem rndMineable = Model.Mine.GetRandomMineableItem();
                viewModel.SetupModel(rndMineable);
                viewModel.SetCallback(ClickHandler);

                if (_generatedDictionary.ContainsKey(rndMineable.Guid))
                {
                    _generatedDictionary[rndMineable.Guid] += 1;
                }
                else
                {
                    _generatedDictionary[rndMineable.Guid] = 1;
                }

                _generatedViewModels.Add(viewModel);
            }

            _generatedOres = tempSpawnPoints.Count;

            tempSpawnPoints.Clear();
            tempSpawnPoints = null;
        }

        public void SetMineOrigin(Mine mine)
        {
            _mineOrigin = mine;

            StartMiniGame();
        }

        protected void StartMiniGame()
        {
            RemoveModel();

            SetupModel(new MineMiniGame(_mineOrigin));

            ClearMiniGame();

            DrawUI();

            this.Show();
        }


        private void ClickHandler(MineableItemViewModel viewModel)
        {
            _clickedOres++;

            Destroy(viewModel.gameObject);

            if (_clickedOres >= _generatedOres)
            {
                MineableItem mineableReward = GetReward();

                _mineableRewardViewModel.gameObject.SetActive(true);
                _mineableRewardViewModel.SetupModel(mineableReward);

                _playerUpgrader.AddCoins(mineableReward.RewardValue);
                _playerUpgrader.UpgradeBodyPowerWithMul(1);
                _playerUpgrader.UpgradeHandsPowerWithMul(1);

                StartCoroutine(WaitForReward());
            }
        }


        private MineableItem GetReward()
        {
            string guid = "";
            int mostMined = 0;

            foreach (var kvp in _generatedDictionary)
            {
                if (kvp.Value > mostMined)
                {
                    guid = kvp.Key;
                    mostMined = kvp.Value;
                }
            }

            return Model.Mine.MineableItems.Find(x => x.Guid == guid);
        }

        private void ClearMiniGame()
        {
            for (int i = _generatedViewModels.Count - 1; i >= 0; i--)
            {
                if (_generatedViewModels[i] != null)
                {
                    Destroy(_generatedViewModels[i].gameObject);
                }
            }

            _generatedDictionary.Clear();

            _generatedOres = 0;
            _clickedOres = 0;

            _generatedViewModels.Clear();
        }


        private List<Transform> GetRandomPointsDistinct()
        {
            List<int> distinctIndices = new List<int>();
            List<Transform> output = new List<Transform>();

            int idx = 0;
            int limiter = 0;
            for (int i = 0; i < Model.RandomOres; i++)
            {
                if (limiter >= 100) break;

                idx = Random.Range(0, _spawnPoints.Count);

                if (distinctIndices.Contains(idx))
                {
                    limiter++;
                    i--;
                    continue;
                }

                distinctIndices.Add(idx);
                output.Add(_spawnPoints[idx]);
            }

            distinctIndices.Clear();
            distinctIndices = null;

            return output;
        }


        private IEnumerator WaitForReward()
        {
            yield return new WaitForSecondsRealtime(2f);

            StartMiniGame();
        }
    }


    public class MineableItemViewModelFactory : DiCreationFactory<MineableItemViewModel> { }
}