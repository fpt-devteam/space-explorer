using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
  public Player player;
  public Image[] healthPoints;
  public Image[] shieldPoints;
  public void Update()
  {
    for (int i = 1; i <= healthPoints.Length; i++)
    {
      healthPoints[i - 1].enabled = i <= player.currentHealth;
    }
    for (int i = 1; i <= shieldPoints.Length; i++)
    {
      shieldPoints[i - 1].enabled = i <= player.currentShield;
    }
  }
}
