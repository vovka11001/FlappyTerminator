using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyDespawnZone _enemyDespawnZone;
    [SerializeField] private Transform _groundTransform;
    
    [SerializeField] private Vector2 _spawnOffset;
    [SerializeField] private float _minOffsetX;
    [SerializeField] private float _maxOffsetX;
    [SerializeField] private float _minOffsetY;
    [SerializeField] private float _maxOffsetY;
    [SerializeField] private float _minOffsetBetweenEnemies;

    [SerializeField] private int _maxSpawnAttempts;
    [SerializeField] private int _enemiesToSpawn;
    [SerializeField] private int _poolDefaultCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Enemy> _enemyPool;
    private readonly List<Enemy> _activeEnemies = new List<Enemy>();

    private void OnEnable()
    {
        _enemyDespawnZone.EnemyReached += ReturnEnemy;
    }

    private void OnDisable()
    {
        _enemyDespawnZone.EnemyReached += ReturnEnemy;
    }
    
    private void Awake()
    {
        _enemyPool = new ObjectPool<Enemy>(
          createFunc: CreateEnemy,
          actionOnGet: EnableEnemy,
         actionOnRelease: DisableEnemy,
          actionOnDestroy: DestroyEnemy,
          collectionCheck: true,
          defaultCapacity: _poolDefaultCapacity,
          maxSize: _poolMaxSize);
    }

    private void Start()
    {
        ResetEnemies();
    }
 
    public void SpawnGroup()
    {
        for (int i = 0; i < _enemiesToSpawn; i++)
            SpawnEnemy();
    }
 
    public void ResetEnemies()
    {
        for (int i = _activeEnemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = _activeEnemies[i];
            _activeEnemies.RemoveAt(i);
            _enemyPool.Release(enemy);
        }
    }
    
    private void ReturnEnemy(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
        _enemyPool.Release(enemy);
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Vector2 position = GetSpawnPosition();

        Enemy enemy = _enemyPool.Get();
        enemy.transform.position = position;
        _activeEnemies.Add(enemy);
    }

    private Vector2 GetRandomPosition()
    {
        float positionX = _groundTransform.position.x +_spawnOffset.x + Random.Range(_minOffsetX, _maxOffsetX);
        float positionY = _groundTransform.position.y +_spawnOffset.y + Random.Range(_minOffsetY, _maxOffsetY);
        
        return new Vector2(positionX, positionY);
    }
    
    private Vector2 GetSpawnPosition()
    {
        Vector2 position = GetRandomPosition();
 
        for (int attempt = 0; attempt < _maxSpawnAttempts && IsEnemyCloseToOther(position); attempt++)
            position = GetRandomPosition();
 
        return position;
    }

    private bool IsEnemyCloseToOther(Vector2 position)
    {
        float minDistance = _minOffsetBetweenEnemies * _minOffsetBetweenEnemies;

        foreach (var activeEnemy in _activeEnemies)
        {
            Vector2 activeEnemyPosition = activeEnemy.transform.position;

            if ((activeEnemyPosition - position).sqrMagnitude < minDistance)
            {
                return true;
            }
        }
        return false;
    }

    private Enemy CreateEnemy()
    {
        return Instantiate(_enemy);
    }

    private void EnableEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void DisableEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void DestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}