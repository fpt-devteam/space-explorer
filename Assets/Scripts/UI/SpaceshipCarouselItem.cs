using HasanSadikin.Carousel;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipCarouselItem : CarouselItem<SpaceshipData>
{
    [SerializeField] Image _image;
    protected override void OnDataUpdated(SpaceshipData data)
    {
        base.OnDataUpdated(data);
        _image.sprite = data.sprite;
    }
}
