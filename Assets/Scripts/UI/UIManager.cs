using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages UI updates and panels.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject instructionsPanel;

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
