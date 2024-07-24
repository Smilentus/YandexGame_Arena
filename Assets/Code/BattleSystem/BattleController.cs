using Dimasyechka.Code.PlayerSystem;
using Dimasyechka.Code.ShopSystem;
using Dimasyechka.Code.Utilities;
using System;
using System.Collections;
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
        public event Action onBattleLeft;


        public ReactiveProperty<TimeSpan> BattleTimeLeft = new ReactiveProperty<TimeSpan>();
        public ReactiveProperty<int> EnemiesInBattle = new ReactiveProperty<int>();


        [SerializeField]
        private List<GameObject> _battleInteractableObjects = new List<GameObject>();


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
        private PlayerBlocker _playerBlocker;

        [Inject]
        public void Construct(
            RuntimePlayerObject runtimePlayerObject,
            PlayerBlocker playerBlocker,
            RuntimePlayerUpgrader playerUpgrader,
            PlayerObjectTag playerObjectTag,
            RuntimeBattleCharacterFactory factory)
        {
            _factory = factory;

            _playerBlocker = playerBlocker;
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
            ToggleBattleInteractables(false);

            _isBattleInProgress = true;

            _lastBattleSettings = battleSettings;

            BattleTimeLeft.Value = TimeSpan.FromSeconds(battleSettings.BattleTimeSeconds);
            EnemiesInBattle.Value = battleSettings.EnemiesInBattle;

            SetupPlayer();
            SpawnEnemies(battleSettings.EnemyProfile, battleSettings.EnemiesInBattle);

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

        private void SpawnEnemies(BattleCharacterProfile profile, int enemies)
        {
            for (int i = 0; i < enemies; i++)
            {
                CreateEnemy(profile);
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

            float x, y, z;
            x = y = z = 0;

            int i = 0;
            Ray ray;

            while (i < 10)
            {
                i++;

                x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
                y = 0.5f;
                z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);

                if (Physics.BoxCast(
                    new Vector3(x, 100, z), 
                    new Vector3(1, 1, 1),
                    Vector3.down, 
                    out RaycastHit hit))
                {
                    if (hit.collider.tag.Equals("Enemy"))
                    {
                        continue;
                    }

                    break;
                }
            }

            return new Vector3(x, y, z);
        }

        private void OnPlayerLost()
        {
            _instantiatedPlayerCharacter.onDead -= OnPlayerLost;

            _isBattleInProgress = false;
            onPlayerLose?.Invoke();

            StopAllCoroutines();
            StartCoroutine(EndBattleWithDelay(2));
        }

        private void OnEnemyDead()
        {
            EnemiesInBattle.Value--;

            if (EnemiesInBattle.Value <= 0)
            {
                Debug.Log($"Battle is over");

                GiveRewardAfterBattle();

                _isBattleInProgress = false;
                onPlayerWin?.Invoke();

                StopAllCoroutines();
                StartCoroutine(EndBattleWithDelay(2));
            }
        }

        private void GiveRewardAfterBattle()
        {
            _playerUpgrader.AddCoins(_lastBattleSettings.WinCoins * (uint)_lastBattleSettings.EnemiesInBattle);
        }


        private void EndBattle()
        {
            _isBattleInProgress = false;

            _instantiatedPlayerCharacter.onDead -= OnPlayerLost;

            DestroyBattlePlayer();
            DestroyEnemies();

            _lastBattleSettings = null;

            onBattleEnded?.Invoke();

            ToggleBattleInteractables(true);
        }

        public void LeaveBattle()
        {
            EndBattle();

            onBattleLeft?.Invoke();
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


        private IEnumerator EndBattleWithDelay(float delaySeconds)
        {
            _playerBlocker.BlockPlayer();

            yield return new WaitForSecondsRealtime(delaySeconds);

            _playerBlocker.UnBlockPlayer();

            EndBattle();
        }


        private void ToggleBattleInteractables(bool value)
        {
            foreach (GameObject go in _battleInteractableObjects)
            {
                go.SetActive(value);
            }
        }
    }


    public class RuntimeBattleCharacterFactory : DiCreationFactory<RuntimeBattleCharacter> { }
}
