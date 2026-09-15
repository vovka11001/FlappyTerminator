using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyShooter _enemyPrefab;
    [SerializeField] private EnemyDespawnZone _despawnZone;
    [SerializeField] private BulletSpawner _enemyBulletPool;
    [SerializeField] private ScoreHandler _scoreHandler;
    [SerializeField] private Transform _ground;

    [SerializeField] private Vector2 _spawnOffset;
    [SerializeField] private float _minOffsetX;
    [SerializeField] private float _maxOffsetX;
    [SerializeField] private float _minOffsetY;
    [SerializeField] private float _maxOffsetY;

    [SerializeField] private int _enemiesInGroup;
    [SerializeField] private float _minDistanceBetweenEnemies;
    [SerializeField] private int _maxSpawnAttempts;

    [SerializeField] private float _minShootDelay;
    [SerializeField] private float _maxShootDelay;

    [SerializeField] private int _poolDefaultCapacity;
    [SerializeField] private int _poolMaxSize;

    private readonly List<EnemyShooter> _activeEnemies = new List<EnemyShooter>();
    private ObjectPool<EnemyShooter> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<EnemyShooter>(
            createFunc: CreateEnemy,
            actionOnGet: EnableEnemy,
            actionOnRelease: DisableEnemy,
            actionOnDestroy: DestroyEnemy,
            collectionCheck: true,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxSize);
    }

    private void OnEnable()
    {
        _despawnZone.EnemyReached += ReturnEnemy;
    }

    private void OnDisable()
    {
        _despawnZone.EnemyReached -= ReturnEnemy;
    }

    private void Start()
    {
        ResetEnemies();
    }

    public void SpawnGroup()
    {
        for (int i = 0; i < _enemiesInGroup; i++)
            SpawnEnemy();
    }

    public void ResetEnemies()
    {
        for (int i = _activeEnemies.Count - 1; i >= 0; i--)
        {
            EnemyShooter enemy = _activeEnemies[i];
            _activeEnemies.RemoveAt(i);
            _pool.Release(enemy);
        }
    }

    private void HandleEnemyKilled(EnemyShooter enemy)
    {
        _scoreHandler.AddScore();
        ReturnEnemy(enemy);
    }

    private void ReturnEnemy(EnemyShooter enemy)
    {
        if (_activeEnemies.Contains(enemy) == false)
            return;

        _activeEnemies.Remove(enemy);
        _pool.Release(enemy);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector2 position = GetSpawnPosition();
        float shootDelay = Random.Range(_minShootDelay, _maxShootDelay);

        EnemyShooter enemy = _pool.Get();
        enemy.transform.position = position;
        enemy.Init(_enemyBulletPool, shootDelay);
        _activeEnemies.Add(enemy);
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 position = GetRandomPosition();

        for (int attempt = 0; attempt < _maxSpawnAttempts && IsTooCloseToActiveEnemies(position); attempt++)
            position = GetRandomPosition();

        return position;
    }

    private Vector2 GetRandomPosition()
    {
        float positionX = _ground.position.x + _spawnOffset.x + Random.Range(_minOffsetX, _maxOffsetX);
        float positionY = _ground.position.y + _spawnOffset.y + Random.Range(_minOffsetY, _maxOffsetY);
        return new Vector2(positionX, positionY);
    }

    private bool IsTooCloseToActiveEnemies(Vector2 position)
    {
        float minDistanceSquared = _minDistanceBetweenEnemies * _minDistanceBetweenEnemies;

        foreach (EnemyShooter activeEnemy in _activeEnemies)
        {
            Vector2 activePosition = activeEnemy.transform.position;

            if ((activePosition - position).sqrMagnitude < minDistanceSquared)
                return true;
        }

        return false;
    }

    private EnemyShooter CreateEnemy()
    {
        EnemyShooter enemy = Instantiate(_enemyPrefab);
        enemy.Died += HandleEnemyKilled;
        return enemy;
    }

    private void EnableEnemy(EnemyShooter enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void DisableEnemy(EnemyShooter enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void DestroyEnemy(EnemyShooter enemy)
    {
        enemy.Died -= HandleEnemyKilled;
        Destroy(enemy.gameObject);
    }
}