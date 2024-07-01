using Cinemachine;
using Dimasyechka.Code.PlayerSystem;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka
{
    public class AthleticsRunningMiniGame : BaseMiniGameController
    {
        [Header("Opponent")]
        [SerializeField]
        private GameObject _opponentAthletePrefab;

        [SerializeField]
        private Transform _opponentSpawnPoint;


        [Header("Tech")]
        [SerializeField]
        private CinemachineVirtualCamera _cineVirtualCamera;

        [SerializeField]
        private Transform _startPoint;

        [SerializeField]
        private Transform _endPoint;


        private float _playerSpeedMultiplier = 1;

        private float _playerSpeedMultiplierDefault = 1f;
        private float _playerSpeedMultiplierMax = 3f;

        private double _finishPosition = 0;

        private AthleticsRunningProgress _progress;

        private RuntimeProgress _opponentProgress;
        private RuntimeProgress _playerProgress;


        private RuntimePlayerObject _runtimePlayerObject;

        [Inject]
        public void Construct(RuntimePlayerObject runtimePlayerObject)
        {
            _runtimePlayerObject = runtimePlayerObject;
        }


        private void Awake()
        {
            LoadProgress();
        }

        private void FixedUpdate()
        {
            if (_isMiniGameRunning)
            {
                UpdateOpponent();
                UpdateMiniGame();
            }
        }


        public void LoadProgress()
        {
            _progress = new AthleticsRunningProgress();

            // TODO: Loading
        }

        public void SaveProgress()
        {
            // TODO: Saving
        }


        public void Setup(double finishPosition)
        {
            _finishPosition = finishPosition;
        }


        protected override void OnMiniGameStarted()
        {
            _opponentProgress = new RuntimeProgress(_finishPosition);
            _playerProgress = new RuntimeProgress(_finishPosition);
        }

        protected override void OnMiniGameEnded()
        {
            
        }


        private void UpdateMiniGame()
        {
            if (_playerProgress.CurrentPosition.Value >= _playerProgress.FinishPosition)
            {
                // Выйграл игрок
                SetPlayerWin();
                EndMiniGame();
            }
            else if (_opponentProgress.CurrentPosition.Value >= _opponentProgress.FinishPosition)
            {
                // Выйграл противник
                SetOpponentWin();
                EndMiniGame();
            }
        }


        private void SetPlayerWin()
        {
            UpdateOpponent();

            SaveProgress();
        }

        private void SetOpponentWin()
        {
            // Усталость добавим большую?
        }


        private void UpdatePlayer()
        {
            _playerSpeedMultiplier = Mathf.Clamp(
                _playerSpeedMultiplier, 
                _playerSpeedMultiplierDefault, 
                _playerSpeedMultiplierMax
            );

            _playerProgress.CurrentPosition.Value += 
                _runtimePlayerObject.RuntimePlayerStats.LegsPower / 100f * _playerSpeedMultiplier * Time.fixedDeltaTime;
        }

        private void UpdateOpponent()
        {
            _opponentProgress.CurrentPosition.Value += _progress.NextOpponentLegsPower / 100f * Time.fixedDeltaTime;
        }

        private void UpgradeOpponents()
        {
            // Каждый противник на 10-15% сильнее предыдущего
            _progress.NextOpponentLegsPower *= Random.Range(1.1f, 1.15f);
        }
    }


    public class RuntimeProgress
    {
        public ReactiveProperty<double> CurrentPosition = new ReactiveProperty<double>();

        public double FinishPosition;


        public RuntimeProgress(double finishPosition)
        {
            FinishPosition = finishPosition;
        }
    }


    [System.Serializable]
    public class AthleticsRunningProgress
    {
        public double NextOpponentLegsPower;


        public AthleticsRunningProgress()
        {
            NextOpponentLegsPower = 100;
        }
    }
}