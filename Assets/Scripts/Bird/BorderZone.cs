using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BorderZone : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}
