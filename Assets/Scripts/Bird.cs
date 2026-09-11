using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BirdMover _birdMover;

    private void OnEnable()
    {
        _inputReader.OnJump += _birdMover.Jump;
    }

    private void OnDisable()
    {
       _inputReader.OnJump -= _birdMover.Jump;
    }
}