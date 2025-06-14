using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles main menu button events.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private string gameplaySceneName = "Gameplay";

    public void OnPlayButton()
    {
        GameManager.Instance.StartGame();
        SceneManager.LoadScene(gameplaySceneName);
    }
    
    public void OnInstructionsButton()
    {
        if (uiManager)
            uiManager.ShowInstructions(true);
    }

    public void OnCloseInstructionsButton()
    {
        if (uiManager)
            uiManager.ShowInstructions(false);
    }
}
