using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
  public Player player;
  public Image[] healthPoints;
  public void Update()
  {
    for (int i = 1; i <= healthPoints.Length; i++)
    {
      healthPoints[i - 1].enabled = i <= player.currentHealth;
    }
  }
}
