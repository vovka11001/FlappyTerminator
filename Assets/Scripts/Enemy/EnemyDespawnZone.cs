using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyDespawnZone : MonoBehaviour
{
    public event Action<EnemyShooter> EnemyReached;
 
    private void OnEnable()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyShooter enemy))
            EnemyReached?.Invoke(enemy);
    }
}
