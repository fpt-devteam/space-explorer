using UnityEngine;
using UnityEngine.UI;

public class ChooseShipPanel : MonoBehaviour
{
    [SerializeField] public Image shipImage;
    [SerializeField] public Sprite[] shipSprites;
    [SerializeField] public Button leftButton, rightButton, selectButton;
    [SerializeField] public GameObject menuPanel;
    [SerializeField] private int currentIndex = 0;

    void Start()
    {
        currentIndex = 0;
        UpdateShip();
        Debug.Log(leftButton);
        Debug.Log(rightButton);
        Debug.Log(selectButton);
        Debug.Log(shipImage);
        leftButton.onClick.AddListener(OnLeft);
        rightButton.onClick.AddListener(OnRight);
        // selectButton.onClick.AddListener(OnSelect);
    }

    public void OnLeft()
    {
        Debug.Log("Left button clicked");
        if (shipSprites == null || shipSprites.Length == 0) return;
        currentIndex = (currentIndex - 1 + shipSprites.Length) % shipSprites.Length;
        UpdateShip();
    }

    public void OnRight()
    {
        Debug.Log("Right button clicked");
        if (shipSprites == null || shipSprites.Length == 0) return;
        currentIndex = (currentIndex + 1) % shipSprites.Length;
        UpdateShip();
    }

    void UpdateShip()
    {
        if (shipSprites != null && shipSprites.Length > 0)
        {
            if (currentIndex < 0) currentIndex = 0;
            if (currentIndex >= shipSprites.Length) currentIndex = shipSprites.Length - 1;

            shipImage.sprite = shipSprites[currentIndex];
        }
        else
        {
            Debug.LogWarning("No ship sprites assigned!");
        }
    }
    // public void OnSelect()
    // {
    //     if (GameManager.Instance != null)
    //     {
    //         GameManager.Instance.OnSelectShip(currentIndex, gameObject);
    //     }
    // }

    // public void ShowPanel()
    // {
    //     gameObject.SetActive(true);
    // }
}