using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdShooter : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private BulletSpawner _bulletPool;

    public void Shoot()
    {
        _bulletPool.Get(_shootPoint.position);
    }
}
