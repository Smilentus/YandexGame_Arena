using Dimasyechka.Code.PlayerSystem;
using Dimasyechka.Code.ShopSystem;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Dimasyechka.Code.BattleSystem
{
    public class BattleController : MonoBehaviour
    {
        public event Action onPlayerWin;
        public event Action onPlayerLose;

        public event Action onBattleStarted;
        public event Action onBattleEnded;


        public ReactiveProperty<TimeSpan> BattleTimeLeft = new ReactiveProperty<TimeSpan>();
        public ReactiveProperty<int> EnemiesInBattle = new ReactiveProperty<int>();


        [SerializeField]
        private RuntimeBattleCharacter _playerBattleCharacterPrefab;

        [SerializeField]
        private RuntimeBattleCharacter _enemyBattleCharacterPrefab;


        private List<RuntimeBattleCharacter> _enemyObjects = new List<RuntimeBattleCharacter>();

        private bool _isBattleInProgress;

        private BattleSettings _lastBattleSettings;


        private RuntimeBattleCharacter _instantiatedPlayerCharacter;
        public RuntimeBattleCharacter InstantiatedPlayerCharacter => _instantiatedPlayerCharacter;


        private RuntimePlayerObject _runtimePlayerObject;
        private RuntimeBattleCharacterFactory _factory;
        private RuntimePlayerUpgrader _playerUpgrader;

        [Inject]
        public void Construct(
            RuntimePlayerObject runtimePlayerObject,
            RuntimeBattleCharacterFactory factory)
        {
            _factory = factory;
            _runtimePlayerObject = runtimePlayerObject;
        }


        private void FixedUpdate()
        {
            if (!_isBattleInProgress) return;

            BattleTimeLeft.Value -= TimeSpan.FromSeconds(Time.fixedDeltaTime);

            if (BattleTimeLeft.Value.TotalSeconds <= 0f)
            {
                OnPlayerLost();
            }
        }


        public void StartBattle(BattleSettings battleSettings)
        {
            _isBattleInProgress = true;

            _lastBattleSettings = battleSettings;

            BattleTimeLeft.Value = TimeSpan.FromSeconds(battleSettings.BattleTimeSeconds);
            EnemiesInBattle.Value = battleSettings.EnemyProfiles.Length;

            SetupPlayer();
            SpawnEnemies(battleSettings.EnemyProfiles);

            onBattleStarted?.Invoke();
        }


        private void SetupPlayer()
        {
            _instantiatedPlayerCharacter = _factory.InstantiateForComponent(
                _playerBattleCharacterPrefab.gameObject,
                _runtimePlayerObject.transform
            );

            _instantiatedPlayerCharacter.SetupCharacter(
                "Player",
                _runtimePlayerObject.RuntimePlayerStats.BodyPower,
                _runtimePlayerObject.RuntimePlayerStats.HandsPower
            );

            _instantiatedPlayerCharacter.onDead += OnPlayerLost;
        }

        private void SpawnEnemies(BattleCharacterProfile[] profiles)
        {
            for (int i = 0; i < profiles.Length; i++)
            {
                CreateEnemy(profiles[i]);
            }
        }

        private void CreateEnemy(BattleCharacterProfile profile)
        {

        }


        private void OnPlayerLost()
        {
            _instantiatedPlayerCharacter.onDead -= OnPlayerLost;

            onPlayerLose?.Invoke();

            EndBattle();
        }

        private void OnEnemyDead()
        {
            EnemiesInBattle.Value--;

            if (EnemiesInBattle.Value <= 0)
            {
                Debug.Log($"Battle is over");

                GiveRewardAfterBattle();

                onPlayerWin?.Invoke();

                EndBattle();
            }
        }

        private void GiveRewardAfterBattle()
        {
            _playerUpgrader.AddCoins(50);
        }


        private void EndBattle()
        {
            _isBattleInProgress = false;

            _instantiatedPlayerCharacter.onDead -= OnPlayerLost;

            DestroyBattlePlayer();
            DestroyEnemies();

            _lastBattleSettings = null;

            onBattleEnded?.Invoke();
        }

        private void DestroyBattlePlayer()
        {
            Destroy(_instantiatedPlayerCharacter.gameObject);
        }

        private void DestroyEnemies()
        {
            for (int i = _enemyObjects.Count - 1; i >= 0; i--)
            {
                Destroy(_enemyObjects[i].gameObject);
            }
        }
    }


    public class RuntimeBattleCharacterFactory : DiCreationFactory<RuntimeBattleCharacter> { }
}
