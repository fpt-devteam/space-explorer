using HasanSadikin.Carousel;
using UnityEngine;

[System.Serializable]
public class ProfileData
{
    public Sprite sprite;
}

public class ProfileCarousel : CarouselController<ProfileData>
{
    private void OnEnable()
    {
        OnItemSelected.AddListener(LogItem);
        OnCurrentItemUpdated.AddListener(LogItem);
    }

    private void LogItem(ProfileData data)
    {
        Debug.Log("Selected Profile: " + data.sprite.name);
    }
}
