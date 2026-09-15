using System;
using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _shootPoint;

    private BulletSpawner _bulletPool;
    private WaitForSeconds _shootWait;
    private Coroutine _shootingCoroutine;

    public event Action<EnemyShooter> Died;

    public void Init(BulletSpawner bulletPool, float shootDelay)
    {
        _bulletPool = bulletPool;
        _shootWait = new WaitForSeconds(shootDelay);

        if (_shootingCoroutine != null)
            StopCoroutine(_shootingCoroutine);

        _shootingCoroutine = StartCoroutine(ShootRepeatedly());
    }

    public void Die()
    {
        Died?.Invoke(this);
    }

    private IEnumerator ShootRepeatedly()
    {
        while (enabled)
        {
            yield return _shootWait;
            _bulletPool.Get(_shootPoint.position);
        }
    }
}