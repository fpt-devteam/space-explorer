using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages UI updates and panels.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject instructionsPanel;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        UpdateScore(ScoreManager.Instance.Score);
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        if (scoreText)
            scoreText.text = $"Score: {score}";
    }

    public void ShowMainMenu(bool show)
    {
        if (mainMenuPanel)
            mainMenuPanel.SetActive(show);
    }

    public void ShowEndGame(bool show)
    {
        if (endGamePanel)
            endGamePanel.SetActive(show);
    }

    public void ShowInstructions(bool show)
    {
        if (instructionsPanel)
            instructionsPanel.SetActive(show);
    }
}
