using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private Bird _bird;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private BulletSpawner _playerBulletPool;
    [SerializeField] private BulletSpawner _enemyBulletPool;

    private void OnEnable()
    {
        _endScreen.EndButtonClicked += RestartGameClick;
        _startScreen.PlayButtonClicked += PlayButtonClick;
        _bird.GameOver += EndGame;
    }

    private void OnDisable()
    {
        _bird.GameOver -= EndGame;
        _endScreen.EndButtonClicked -= RestartGameClick;
        _startScreen.PlayButtonClicked -= PlayButtonClick;
    }

    private void Start()
    {
        Time.timeScale = 0;
        _startScreen.Open();
        _endScreen.Close();
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        _endScreen.Open();
        _enemySpawner.ResetEnemies();
        _playerBulletPool.Clear();
        _enemyBulletPool.Clear();
    }

    private void RestartGameClick()
    {
        _endScreen.Close();
        StartGame();
    }

    private void PlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        Time.timeScale = 1;
        _bird.Reset();
        _enemySpawner.SpawnGroup();
    }
}