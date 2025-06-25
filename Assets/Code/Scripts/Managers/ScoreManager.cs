using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
  public static ScoreManager Instance { get; private set; }

  [Header("UI")]
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI highScoreText;

  private int score = 0;
  private int highScore = 0;

  public int Score => score;
  public int HighScore => highScore;

  private void LoadHighScore()
  {
    // Load high score from persistent storage or LeaderboardManager
    // For now, we can just set it to a default value
    // LeaderboardManager.Instance.LoadLeaderboardData();
    // // Load high score from LeaderboardManager
    // highScore = LeaderboardManager.Instance.GetHighScore();
    // Debug.Log($"ScoreManager: Loaded high score: {highScore}");
    // Debug.Log($"Num Leaderboard Entries: {LeaderboardManager.Instance.leaderboardData.topEntries.Count}");
  }


  private void Awake()
  {
    Debug.Log("ScoreManager: Awake called");
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    LoadHighScore();
  }

  public void AddPoints(int points)
  {
    score += points;
  }

  public void ResetScore()
  {
    score = 0;
  }
}
