using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum GameState { MainMenu, Playing, Pausing, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }
    public event Action<int, bool> OnGameEnded;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        CurrentState = GameState.MainMenu;
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        ScoreManager.Instance?.ResetScore();
    }

    public void EndGame()
    {
        CurrentState = GameState.GameOver;

        // int finalScore = ScoreManager.Instance?.GetScore() ?? 0;
        // bool isHighScore = false;

        // if (HighScoreManager.Instance != null)
        // {
        //     isHighScore = HighScoreManager.Instance.AddScore(finalScore);
        // }

        // OnGameEnded?.Invoke(finalScore, isHighScore);

        // Debug.Log(isHighScore
        //     ? $"🎉 NEW HIGH SCORE! {finalScore}"
        //     : $"Game Over. Final Score: {finalScore}");
    }

    public void ReturnToMenu()
    {
        CurrentState = GameState.MainMenu;
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Playing)
        {
            CurrentState = GameState.Pausing;
            Time.timeScale = 0f;

            //Stop all active spawners
            AsteroidSpawner spawners = FindObjectOfType<AsteroidSpawner>();
            if (spawners != null)
            {
                spawners.StopSpawning();
            }
        }
    }
    public void ResumeGame()
    {
        if (CurrentState == GameState.Pausing)
        {
            CurrentState = GameState.Playing;
            Time.timeScale = 1f;
        }
    }

    public void RestartGame()
    {
        CurrentState = GameState.Playing;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
