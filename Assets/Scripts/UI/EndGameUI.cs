using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Handles end game UI logic and button events.
/// </summary>
public class EndGameUI : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text highScoreStatusText;
    [SerializeField] private Text currentHighScoreText;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameEnded += OnGameEnded;
        }
        UpdateDisplay();
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnGameEnded -= OnGameEnded;
        }
    }

    private void OnGameEnded(int finalScore, bool isHighScore)
    {
        UpdateDisplay(finalScore, isHighScore);
    }

    private void UpdateDisplay(int? finalScore = null, bool? isHighScore = null)
    {
        // Update final score
        if (finalScoreText != null)
        {
            int scoreToShow = finalScore ?? (ScoreManager.Instance != null ? ScoreManager.Instance.Score : 0);
            finalScoreText.text = "Final Score: " + scoreToShow.ToString("N0");
        }

        // Update high score status
        if (highScoreStatusText != null)
        {
            if (isHighScore.HasValue && isHighScore.Value)
            {
                highScoreStatusText.text = "🎉 NEW HIGH SCORE! 🎉";
                highScoreStatusText.color = Color.yellow;
            }
            else
            {
                highScoreStatusText.text = "";
            }
        }

        // Update current high score display
        if (currentHighScoreText != null && HighScoreManager.Instance != null)
        {
            int highestScore = HighScoreManager.Instance.HighestScore;
            currentHighScoreText.text = "Best Score: " + highestScore.ToString("N0");
        }
    }

    public void OnMainMenuButton()
    {
        GameManager.Instance.ReturnToMenu();
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void OnRestartButton()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
