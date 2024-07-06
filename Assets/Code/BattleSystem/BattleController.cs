using Dimasyechka.Code.PlayerSystem;
using Dimasyechka.Code.ShopSystem;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Experimental.AI;
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
        private GameObject _spawnBounds;


        [SerializeField]
        private RuntimeBattleCharacter _playerBattleCharacterPrefab;


        private List<RuntimeBattleCharacter> _enemyObjects = new List<RuntimeBattleCharacter>();

        private bool _isBattleInProgress;
        public bool IsBattleInProgress => _isBattleInProgress;

        private BattleSettings _lastBattleSettings;


        private RuntimeBattleCharacter _instantiatedPlayerCharacter;
        public RuntimeBattleCharacter InstantiatedPlayerCharacter => _instantiatedPlayerCharacter;


        private RuntimePlayerObject _runtimePlayerObject;
        private PlayerObjectTag _playerObjectTag;
        private RuntimeBattleCharacterFactory _factory;
        private RuntimePlayerUpgrader _playerUpgrader;

        [Inject]
        public void Construct(
            RuntimePlayerObject runtimePlayerObject,
            RuntimePlayerUpgrader playerUpgrader,
            PlayerObjectTag playerObjectTag,
            RuntimeBattleCharacterFactory factory)
        {
            _factory = factory;

            _playerUpgrader = playerUpgrader;
            _playerObjectTag = playerObjectTag;
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
                _playerObjectTag.transform
            );

            _instantiatedPlayerCharacter.SetupCharacter(
                "Player",
                _runtimePlayerObject.RuntimePlayerStats.MaxHealth,
                _runtimePlayerObject.RuntimePlayerStats.Damage
            );

            _instantiatedPlayerCharacter.SetupAnimator(_playerObjectTag.AnimatorReference);

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
            RuntimeBattleCharacter enemy = _factory.InstantiateForComponent(
                profile.CharacterPrefab.gameObject,
                GetRandomSpawnPosition()
            );

            enemy.SetupCharacter(profile.CharacterName, profile.Health, profile.Damage);
            enemy.onDead += OnEnemyDead;

            _enemyObjects.Add(enemy);
        }


        private Vector3 GetRandomSpawnPosition()
        {
            Bounds bounds = _spawnBounds.GetComponent<Renderer>().bounds;


            float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            float y = 0;  
            float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);

            Ray ray = new Ray(new Vector3(x, 100, z), Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                y = hit.point.y;
            }

            return new Vector3(x, y, z);
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
            _playerUpgrader.AddCoins(100);
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

            _enemyObjects.Clear();
        }
    }


    public class RuntimeBattleCharacterFactory : DiCreationFactory<RuntimeBattleCharacter> { }
}
