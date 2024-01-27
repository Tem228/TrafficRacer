using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesService : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField]
    private float _minEnemySpawnDelay;
    [SerializeField]
    private float _maxEnemySpawnDelay;

    [SerializeField]
    private float _diactivateEnemyDistance;

    [SerializeField]
    private Vector3 _enemiesSpawnOffset;
    [SerializeField]
    private Enemy[] _enemiesPrefabs;
    [SerializeField]
    private Transform _enemiesParent;

    private Vector3[] _spawnPositions;

    private Enemy[] _enemies;

    [Header("Services")]
    [SerializeField]
    private RoadsService _roadsService;
    [SerializeField]
    private GameController _gameController;

    public static EnemiesService Instance { get; private set; }

    private const int ENEMIES_AMOUNT = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeSpawnPositions();

        SpawnEnemies();
    }

    private void OnEnable()
    {
        _gameController.GameStateUpdated += OnGameStateUpdated;
    }

    private void OnDisable()
    {
        _gameController.GameStateUpdated -= OnGameStateUpdated;
    }

    private void InitializeSpawnPositions()
    {
        Vector3 lastRoadPosition = _roadsService.LastRoad.transform.position;

        Vector3 middlePosition = _roadsService.LastRoad.transform.position + _enemiesSpawnOffset;

        Vector3 rightPosition = middlePosition - Vector3.right * 3;

        Vector3 leftPosition = middlePosition + Vector3.right * 3;

        _spawnPositions = new Vector3[]
        {
            leftPosition,
            middlePosition,
            rightPosition,
        };
    }

    private void SpawnEnemies()
    {
        _enemies = new Enemy[ENEMIES_AMOUNT];

        for (int i = 0; i < ENEMIES_AMOUNT; i++)
        {
            Enemy enemyPrefab = _enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Length)];

            Enemy enemy = Instantiate(enemyPrefab, _spawnPositions[1], Quaternion.identity, _enemiesParent);

            enemy.SetVisible(false);

            _enemies[i] = enemy;
        }
    }

    private IEnumerator ShowEnemies()
    {
        while (_gameController.State == GameState.Playing)
        {
            float delay = Random.Range(_minEnemySpawnDelay, _maxEnemySpawnDelay);

            yield return new WaitForSeconds(delay);

            Enemy enemy = GetAvailableEnemy();

            if(enemy == null)
            {
                continue;
            }

            enemy.transform.position = _spawnPositions[Random.Range(0, _spawnPositions.Length)];

            enemy.SetVisible(true);

        }
    }
   
    private Enemy GetAvailableEnemy()
    {
        List<Enemy> availableEnemies = new List<Enemy>();

        for(int i = 0; i < _enemies.Length; i++)
        {
            Enemy enemy = _enemies[i];

            if(!enemy.gameObject.activeSelf 
            || enemy.transform.position.z <= _diactivateEnemyDistance)
            {
                availableEnemies.Add(enemy);
            }
        }

        if(availableEnemies.Count == 0)
        {
            return null;
        }

        return availableEnemies[Random.Range(0, availableEnemies.Count)];
    }

    private void OnGameStateUpdated(GameState state)
    {
        if (state == GameState.Playing)
        {
            StartCoroutine(ShowEnemies());
        }
    }
}
