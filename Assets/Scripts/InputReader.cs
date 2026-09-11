using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _jumpKey = KeyCode.UpArrow;
    
    public event Action OnJump;

    private void Update()
    {
        if (Input.GetKeyDown(_jumpKey))
            OnJump?.Invoke();
    }
}