using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public static PlayerManager Instance { get; private set; }

  [Header("Player Ships")]
  public GameObject[] playerShips;
  private GameObject currentPlayerShip;
  private PlayerController playerController;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
  }

  private void Start()
  {
    if (playerShips.Length > 0)
    {
      SelectPlayerShip(0);
    }
    else
    {
      Debug.LogError("No player ships available!");
    }
  }

  public void SelectPlayerShip(int shipIndex)
  {
    if (shipIndex < 0 || shipIndex >= playerShips.Length) return;

    if (currentPlayerShip != null)
    {
      Destroy(currentPlayerShip);
    }

    currentPlayerShip = Instantiate(playerShips[shipIndex], transform.position, Quaternion.identity);
    playerController = currentPlayerShip.GetComponent<PlayerController>();

    SetupShipAttributes(shipIndex);
  }

  private void SetupShipAttributes(int shipIndex)
  {
    switch (shipIndex)
    {
      case 0:
        currentPlayerShip.GetComponent<Player>().currentHealth = currentPlayerShip.GetComponent<Player>().baseHealth;
        currentPlayerShip.GetComponent<Player>().currentShield = currentPlayerShip.GetComponent<Player>().baseShield;
        break;
    }
  }
}