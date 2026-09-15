using System;
using UnityEngine;

public class ScoreHandler : MonoBehaviour, IInteractable
{
    private int _score = 0;

    public event Action<int> ScoreChanged;
    
    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged?.Invoke(_score);
    }
}
