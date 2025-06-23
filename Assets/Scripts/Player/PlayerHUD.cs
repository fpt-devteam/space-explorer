using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
  public Image[] healthPoints;
  public Image[] shieldPoints;

  private void Start()
  {
    if (healthPoints == null || healthPoints.Length == 0)
    {
      Debug.LogError("Health points not assigned in PlayerHUD.");
    }

    if (shieldPoints == null || shieldPoints.Length == 0)
    {
      Debug.LogError("Shield points not assigned in PlayerHUD.");
    }
  }

  public void ShowHealth(int healthCounter)
  {
    for (int i = 0; i < healthPoints.Length; i++)
    {
      healthPoints[i].enabled = i < healthCounter;
    }
  }

  public void ShowShield(int shieldCounter)
  {
    for (int i = 0; i < shieldPoints.Length; i++)
    {
      shieldPoints[i].enabled = i < shieldCounter;
    }
  }
}
