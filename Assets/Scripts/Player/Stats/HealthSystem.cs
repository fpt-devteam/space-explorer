using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
  public Player player;
  public Slider HealthSlider;
  private float displayHealth = 1f;
  [SerializeField] private Image fillImage;
  public void Update()
  {
    if (HealthSlider != null)
    {
      float target = player.currentHealth / player.baseHealth;
      displayHealth = Mathf.Lerp(displayHealth, target, Time.deltaTime * 10f);
      HealthSlider.value = displayHealth;

      UpdateColor(displayHealth);
    }
  }
  public void UseHealth(float amount)
  {
    player.currentHealth = Mathf.Max(0f, player.currentHealth - amount);
  }

  private void UpdateColor(float HealthPercent)
  {
    Color color;

    if (HealthPercent > 0.6f)
      color = Color.green;
    else if (HealthPercent > 0.3f)
      color = Color.yellow;
    else
      color = Color.red;

    fillImage.color = color;
  }
}
