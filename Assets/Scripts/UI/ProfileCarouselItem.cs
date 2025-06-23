using HasanSadikin.Carousel;
using UnityEngine;
using UnityEngine.UI;

public class ProfileCarouselItem : CarouselItem<ProfileData>
{
    [SerializeField] Image _image;
    protected override void OnDataUpdated(ProfileData data)
    {
        base.OnDataUpdated(data);
        _image.sprite = data.sprite;
    }
}
