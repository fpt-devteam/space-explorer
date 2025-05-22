using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles end game UI logic and button events.
/// </summary>
public class EndGameUI : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void OnEnable()
    {
        if (finalScoreText)
            finalScoreText.text = $"Final Score: {ScoreManager.Instance.Score}";
    }

    public void OnMainMenuButton()
    {
        GameManager.Instance.ReturnToMenu();
        SceneLoader.Instance.LoadScene(mainMenuSceneName);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
