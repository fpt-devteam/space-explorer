using UnityEngine;
using UnityEngine.UI;

public class ShieldSystem : MonoBehaviour
{
  public Player player;
  public Image[] shieldPoints;
  
  public void Update()
  {
    for (int i = 1; i <= shieldPoints.Length; i++)
    {
      shieldPoints[i - 1].enabled = i <= player.currentShield;
    }
  }
}
