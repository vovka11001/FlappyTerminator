using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private KeyCode _jumpKey = KeyCode.UpArrow;
    private KeyCode _shootKey = KeyCode.Space;

    public event Action OnJump;
    public event Action OnShoot;

    private void Update()
    {
        if (Input.GetKeyDown(_jumpKey))
            OnJump?.Invoke();

        if (Input.GetKeyDown(_shootKey))
            OnShoot?.Invoke();
    }
}