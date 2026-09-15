using System;
using UnityEngine;

public class BirdCollisionHandler : MonoBehaviour
{
    public event Action<Iinteractable> CollisionDetected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Iinteractable interactable))
            CollisionDetected?.Invoke(interactable);
    }
}