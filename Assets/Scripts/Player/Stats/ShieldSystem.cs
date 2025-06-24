using UnityEngine;

public class ShieldSystem : MonoBehaviour
{
  private Player player;
  private GameObject shieldObject;
  private Animator animator;

  public bool IsShieldActive => player.currentShield > 0;

  private void Awake()
  {
    animator = GetComponent<Animator>();
    shieldObject = GameObject.Find("Shield");
    player = GameObject.Find("Spaceship").GetComponent<Player>();

    if (animator == null)
    {
      Debug.LogError("Animator component not found on the ShieldSystem GameObject.");
    }
    if (shieldObject != null)
    {
      Debug.Log("Shield initialized.");
    }
    else
    {
      Debug.LogError("ShieldObject not found in the scene.");
    }
  }

  private void Update()
  {
    if (shieldObject == null)
    {
      Debug.LogError("ShieldObject is null. Please ensure it is assigned in the scene.");
      return;
    }

    if (player == null)
    {
      Debug.LogError("Player component not found. Please ensure the Player script is attached to the GameObject.");
      return;
    }

    if (IsShieldActive)
    {
      ActivateShield();
    }
    else
    {
      DeactivateShield();
    }
  }

  public void ActivateShield()
  {
    animator.Play("Battlecruise_Shield");
    Debug.Log("Shield activated.");
  }

  public void DeactivateShield()
  {
    animator.Play("New State");
    Debug.Log("Shield deactivated.");
  }
}