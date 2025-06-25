using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarouselManager : MonoBehaviour
{
    [Header("Tab Buttons")]
    public Button spaceshipButton;
    public Button mapButton;

    [Header("Carousels")]
    public GameObject spaceshipCarousel;
    public GameObject mapCarousel;

    [Header("Level Text")]
    [SerializeField] public TextMeshProUGUI levelText;
    [Header("Coin Text")]
    [SerializeField] public TextMeshProUGUI coinText;

    void Start()
    {
        spaceshipButton.Select();
        spaceshipButton.onClick.AddListener(ShowSpaceshipCarousel);
        mapButton.onClick.AddListener(ShowMapCarousel);

        UpdateText();
        ShowSpaceshipCarousel();
    }

    private void UpdateText()
    {
        if (PlayerManager.Instance == null)
        {
            Debug.LogError("PlayerManager is null");
            return;
        }
        if (PlayerManager.Instance.playerData == null)
        {
            Debug.LogError("PlayerData is null");
            return;
        }
        Debug.Log("PlayerData level: " + PlayerManager.Instance.playerData.level);
        Debug.Log("PlayerData numStars: " + PlayerManager.Instance.playerData.numStars);
        levelText.text = PlayerManager.Instance.playerData.level.ToString();
        coinText.text = PlayerManager.Instance.playerData.numStars.ToString();
    }

    public void ShowSpaceshipCarousel()
    {
        spaceshipCarousel.SetActive(true);
        mapCarousel.SetActive(false);
    }

    public void ShowMapCarousel()
    {
        spaceshipCarousel.SetActive(false);
        mapCarousel.SetActive(true);
    }
}
