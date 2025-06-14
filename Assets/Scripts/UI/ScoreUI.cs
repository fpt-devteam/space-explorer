using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    private void Awake()
    {
        scoreText = transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        highScoreText = transform.Find("HighScoreText").GetComponent<TextMeshProUGUI>();

        if (highScoreText == null)
            Debug.LogError("ScoreUI: HighScoreText TextMeshProUGUI component not found.");
        if (scoreText == null)
            Debug.LogError("ScoreUI: TextMeshProUGUI component not found.");
    }

    private void Start()
    {
        UpdateHighScoreDisplay();
    }

    private void OnEnable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
            UpdateScoreUI(ScoreManager.Instance.Score);
        }

        // Subscribe to high score updates
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.OnHighScoresUpdated += OnHighScoresUpdated;
        }
    }

    private void OnDisable()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreUI;

        if (HighScoreManager.Instance != null)
            HighScoreManager.Instance.OnHighScoresUpdated -= OnHighScoresUpdated;
    }

    private void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
            scoreText.text = "Score: " + newScore;
    }

    private void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            var highScoreManager = HighScoreManager.Instance;
            if (highScoreManager != null)
            {
                int highestScore = highScoreManager.HighestScore;
                highScoreText.text = "High Score: " + highestScore;
            }
            else
            {
                highScoreText.text = "High Score: 0";
            }
        }
    }

    private void OnHighScoresUpdated(System.Collections.Generic.List<HighScoreEntry> updatedScores)
    {
        UpdateHighScoreDisplay();
    }
}
