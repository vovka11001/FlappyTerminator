using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override Vector2 Direction => Vector2.right;

    protected override bool IsValidTarget(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyShooter enemy) == false)
            return false;

        enemy.Die();
        return true;
    }
}