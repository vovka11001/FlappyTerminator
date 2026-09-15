using System;
using UnityEngine;

public class ScoreHandler : MonoBehaviour, Iinteractable
{
    private int _score = 0;

    public event Action<int> ScoreChanged;
    
    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }
}
