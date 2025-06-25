using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
  [Header("UI")]
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI highScoreText;

  private void Update()
  {
    if (scoreText != null)
      scoreText.text = $"Score: {ScoreManager.Instance.Score}";
    if (highScoreText != null)
      highScoreText.text = $"High Score: {ScoreManager.Instance.HighScore}";
  }
}
