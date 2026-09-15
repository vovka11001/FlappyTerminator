using UnityEngine;

public class EnemyBullet : Bullet, IInteractable
{
    protected override Vector2 Direction => Vector2.left;

    protected override bool IsValidTarget(Collider2D other)
    {
        return other.TryGetComponent(out Bird _);
    }
}