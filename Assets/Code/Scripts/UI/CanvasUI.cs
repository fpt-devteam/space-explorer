using UnityEngine;
public class CanvasUI : MonoBehaviour
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject pauseMenu;
  [SerializeField] private GameObject gameOverMenu;
  [SerializeField] private GameObject winMenu;
  [SerializeField] private GameObject settingsMenu;
  [SerializeField] private GameObject playerHUD;

  private void Update()
  {
    UpdateHealthBar(player.GetComponent<Player>().currentHealth);
    UpdateShieldBar(player.GetComponent<Player>().currentShield);
  }

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
    SoundManager.Instance.PlaySFX(SoundManager.Instance.gameOver);
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
    SoundManager.Instance.PlaySFX(SoundManager.Instance.victory);
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