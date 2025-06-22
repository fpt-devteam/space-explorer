using HasanSadikin.Carousel;
using UnityEngine;
[System.Serializable]
public class MapData
{
    public Sprite sprite;
}
public class MapCarousel : CarouselController<MapData>
{
    private void OnEnable()
    {
        OnItemSelected.AddListener(LogItem);
        OnCurrentItemUpdated.AddListener(LogItem);
    }
    private void LogItem(MapData data)
    {
        Debug.Log("Selected Map: " + data.sprite.name);
    }
}
