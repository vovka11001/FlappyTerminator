using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private const int InitialScore = 0;

    [SerializeField] private ScoreHandler _scoreHandler;
    [SerializeField] private TMP_Text _scoreText;

    private void OnEnable()
    {
        _scoreHandler.ScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        _scoreHandler.ScoreChanged -= UpdateScore;
    }

    private void Start()
    {
        UpdateScore(InitialScore);
    }

    private void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}