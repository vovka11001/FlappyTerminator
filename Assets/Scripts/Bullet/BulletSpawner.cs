using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private readonly List<Bullet> _activeBullets = new List<Bullet>();
    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
            createFunc: CreateBullet,
            actionOnGet: EnableBullet,
            actionOnRelease: DisableBullet,
            actionOnDestroy: DestroyBullet,
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    public void Get(Vector3 position)
    {
        Bullet bullet = _pool.Get();
        bullet.transform.position = position;
        bullet.Launch();
        _activeBullets.Add(bullet);
    }

    public void Clear()
    {
        for (int i = _activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = _activeBullets[i];
            _activeBullets.RemoveAt(i);
            _pool.Release(bullet);
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform);
        bullet.Finished += ReturnBullet;
        return bullet;
    }

    private void EnableBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void DisableBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet bullet)
    {
        bullet.Finished -= ReturnBullet;
        Destroy(bullet.gameObject);
    }

    private void ReturnBullet(Bullet bullet)
    {
        _activeBullets.Remove(bullet);
        _pool.Release(bullet);
    }
}