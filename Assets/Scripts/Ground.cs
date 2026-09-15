using UnityEngine;
[RequireComponent (typeof(BoxCollider2D))]
public class Ground : MonoBehaviour,IInteractable
{
    private BoxCollider2D _boxCollider;

    private void OnEnable()
    {
        _boxCollider.isTrigger = true;
    }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }
}