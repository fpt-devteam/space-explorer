using HasanSadikin.Carousel;
using UnityEngine;
using UnityEngine.UI;

public class MapCarouselItem : CarouselItem<MapData>
{
    [SerializeField] Image _image;
    protected override void OnDataUpdated(MapData data)
    {
        base.OnDataUpdated(data);
        _image.sprite = data.sprite;
    }
}
