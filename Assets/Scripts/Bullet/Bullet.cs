using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody2D;

    public event Action<Bullet> Finished;

    protected abstract Vector2 Direction { get; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsValidTarget(other))
            Finish();
        else if (other.TryGetComponent(out BorderZone _))
            Finish();
    }

    public void Launch()
    {
        _rigidbody2D.velocity = Direction.normalized * _speed;
    }

    protected abstract bool IsValidTarget(Collider2D other);

    private void Finish()
    {
        Finished?.Invoke(this);
    }
}
