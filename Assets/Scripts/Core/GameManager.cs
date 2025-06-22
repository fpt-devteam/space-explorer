using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public enum GameState { MainMenu, Playing, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    // Events
    public event Action<int, bool> OnGameEnded; // finalScore, isHighScore

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
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
    }

    public void EndGame()
    {
        CurrentState = GameState.GameOver;

        if (ScoreManager.Instance != null && HighScoreManager.Instance != null)
        {
            int finalScore = ScoreManager.Instance.Score;
            bool isHighScore = HighScoreManager.Instance.AddScore(finalScore);

            OnGameEnded?.Invoke(finalScore, isHighScore);

            if (isHighScore)
            {
                Debug.Log($"🎉 NEW HIGH SCORE! {finalScore}");
            }
            else
            {
                Debug.Log($"Game Over. Final Score: {finalScore}");
            }
        }
    }

    public void ReturnToMenu()
    {
        CurrentState = GameState.MainMenu;
    }

    public void RestartGame()
    {
        StartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SetPanelActive(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel is null, it.");
        }
    }
}
