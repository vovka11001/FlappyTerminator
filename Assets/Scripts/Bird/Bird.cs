using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private BirdMover _birdMover;
    [SerializeField] private BirdShooter _birdShooter;
    [SerializeField] private BirdCollisionHandler _birdCollisionHandler;
    [SerializeField] private ScoreHandler _scoreHandler;

    public event Action GameOver;

    private void OnEnable()
    {
        _inputReader.OnJump += _birdMover.Jump;
        _inputReader.OnShoot += _birdShooter.Shoot;
        _birdCollisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _inputReader.OnJump -= _birdMover.Jump;
        _inputReader.OnShoot -= _birdShooter.Shoot;
        _birdCollisionHandler.CollisionDetected -= ProcessCollision;
    }

    public void Reset()
    {
        _birdMover.Reset();
        _scoreHandler.Reset();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Ground or EnemyShooter or EnemyBullet)
            GameOver?.Invoke();
    }
}