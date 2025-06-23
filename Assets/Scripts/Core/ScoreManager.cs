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

  private void Awake()
  {
    Debug.Log("ScoreManager: Awake called");
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    UpdateUI();
  }

  public void AddPoints(int points)
  {
    score += points;

    UpdateUI();

    Debug.Log($"ScoreManager: Added {points} points. New score: {score}");
  }

  public void ResetScore()
  {
    score = 0;
    UpdateUI();
  }

  private void UpdateUI()
  {
    if (scoreText != null)
      scoreText.text = $"Score: {score}";
    if (highScoreText != null)
      highScoreText.text = $"High Score: {highScore}";
  }
}
