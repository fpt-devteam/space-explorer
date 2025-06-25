using HasanSadikin.Carousel;
using UnityEngine;
[System.Serializable]
public class SpaceshipData
{
    public Sprite sprite;
}
public class SpaceshipCarousel : CarouselController<SpaceshipData>
{
    private void OnEnable()
    {
        OnItemSelected.AddListener(LogItem);
        OnCurrentItemUpdated.AddListener(LogItem);
    }
    private void LogItem(SpaceshipData data)
    {
        Debug.Log("Selected Spaceship: " + data.sprite.name);
    }
}
