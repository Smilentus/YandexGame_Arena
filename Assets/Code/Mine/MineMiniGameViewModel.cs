using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Code.Windows;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.Mine
{
    public class MineMiniGameViewModel : BaseShopViewModel<MineMiniGame>
    {
        [SerializeField]
        private MineableItemViewModel _mineableItemViewModelPrefab;

        [SerializeField]
        private List<Transform> _spawnPoints = new List<Transform>();


        private MineableItemViewModelFactory _factory;


        private int _generatedOres = 0;
        private int _clickedOres = 0;

        private List<MineableItemViewModel> _generatedViewModels = new List<MineableItemViewModel>();


        [Inject]
        public void Construct(MineableItemViewModelFactory factory)
        {
            _factory = factory;
        }


        public void SetMiniGame(MineMiniGame mineMiniGame)
        {
            SetupModel(mineMiniGame);

            ClearMiniGame();

            DrawUI();

            this.Show();
        }

        private void DrawUI()
        {
            List<Transform> tempSpawnPoints = GetRandomPointsDistinct();

            for (int i = 0; i < tempSpawnPoints.Count; i++)
            {
                MineableItemViewModel viewModel =
                    _factory.InstantiateForComponent(_mineableItemViewModelPrefab.gameObject, tempSpawnPoints[i]);

                viewModel.SetupModel(Model.Mine.GetRandomMineableItem());
                viewModel.SetCallback(ClickHandler);

                _generatedViewModels.Add(viewModel);
            }

            _generatedOres = tempSpawnPoints.Count;

            tempSpawnPoints.Clear();
            tempSpawnPoints = null;
        }


        private void ClickHandler(MineableItemViewModel viewModel)
        {
            _clickedOres++;

            Destroy(viewModel.gameObject);

            if (_clickedOres >= _generatedOres)
            {
                ClearMiniGame();

                Hide();
            }
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

    }


    public class MineableItemViewModelFactory : DiCreationFactory<MineableItemViewModel> { }
}