using UnityEngine;
public class CanvasManager : MonoBehaviour
{
  [SerializeField] private GameObject pauseMenu;
  [SerializeField] private GameObject gameOverMenu;
  [SerializeField] private GameObject winMenu;
  [SerializeField] private GameObject settingsMenu;
  [SerializeField] private GameObject playerHUD;

  public void ShowPauseMenu()
  {
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
  }
  public void HidePauseMenu()
  {
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
  }
  public void ShowGameOverMenu()
  {
    gameOverMenu.SetActive(true);
    Time.timeScale = 0f;
  }
  public void HideGameOverMenu()
  {
    gameOverMenu.SetActive(false);
    Time.timeScale = 1f;
  }

  public void ShowWinMenu()
  {
    winMenu.SetActive(true);
    Time.timeScale = 0f;
  }
  public void HideWinMenu()
  {
    winMenu.SetActive(false);
    Time.timeScale = 1f;
  }

  public void ToggleSettingsMenu()
  {
    if (settingsMenu.activeSelf)
    {
      settingsMenu.SetActive(false);
    }
    else
    {
      settingsMenu.SetActive(true);
    }
  }
  public void UpdateHealthBar(int healthAmount)
  {
    if (playerHUD != null)
    {
      playerHUD.GetComponent<PlayerHUD>().ShowHealth(healthAmount);
    }
  }
  public void UpdateShieldBar(int shieldAmount)
  {
    if (playerHUD != null)
    {
      playerHUD.GetComponent<PlayerHUD>().ShowShield(shieldAmount);
    }
  }
}