using UnityEngine;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
  public Player player;
  public Slider staminaSlider;
  public float regenRate = 10f;
  private float displayStamina = 1f;
  [SerializeField] private Image fillImage;
  public void Update()
  {
    RegenerateStamina();
    UpdateUI();
  }
  public void UseStamina(float amount)
  {
    player.currentStamina = Mathf.Max(0f, player.currentStamina - amount);
  }
  private void RegenerateStamina()
  {
    if (player.currentStamina < player.baseStamina)
    {
      float maxStamina = player.baseStamina;
      if (maxStamina <= 0f) return;

      player.currentStamina += regenRate * Time.deltaTime;
      player.currentStamina = Mathf.Min(player.currentStamina, maxStamina);
    }
  }
  private void UpdateUI()
  {
    if (staminaSlider != null)
    {
      float target = player.currentStamina / player.baseStamina;
      displayStamina = Mathf.Lerp(displayStamina, target, Time.deltaTime * 10f);
      staminaSlider.value = displayStamina;

      UpdateColor(displayStamina);
    }
  }
  private void UpdateColor(float staminaPercent)
  {
    Color color;

    if (staminaPercent > 0.6f)
      color = Color.green;
    else if (staminaPercent > 0.3f)
      color = Color.yellow;
    else
      color = Color.red;

    fillImage.color = color;
  }
}
