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

    void Start()
    {
        spaceshipButton.Select();
        spaceshipButton.onClick.AddListener(ShowSpaceshipCarousel);
        mapButton.onClick.AddListener(ShowMapCarousel);

        ShowSpaceshipCarousel();
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
