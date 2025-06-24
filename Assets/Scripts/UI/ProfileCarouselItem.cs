using HasanSadikin.Carousel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileCarouselItem : CarouselItem<ProfileData>
{
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _playerNameText;
    [SerializeField] TextMeshProUGUI _playerIdText;
    protected override void OnDataUpdated(ProfileData data)
    {
        base.OnDataUpdated(data);
        print(data?.playerId + " - " + data?.playerName);

        _image.sprite = data.sprite;
        _playerNameText.text = data.playerName;
    }
}
