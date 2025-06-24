using System.Collections.Generic;
using System.Linq;
using HasanSadikin.Carousel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ProfileData
{
    public string playerId;
    public string playerName;
    public Sprite sprite = null;
}

public class ProfileCarousel : MonoBehaviour
{
    [SerializeField] private Sprite defaultProfileSprite;
    private ProfileCarouselController carouselController;
    [SerializeField] private ProfileCarouselItem carouselItemPrefab;
    [SerializeField] private bool isInfinity = false;
    [SerializeField] private Origin childOrigin = Origin.Center;

    private List<ProfileData> profiles = new List<ProfileData>();

    // Events để expose ra ngoài
    public UnityEvent<ProfileData> OnItemSelected = new UnityEvent<ProfileData>();
    public UnityEvent<ProfileData> OnCurrentItemUpdated = new UnityEvent<ProfileData>();
    public UnityEvent OnPrev = new UnityEvent();
    public UnityEvent OnNext = new UnityEvent();

    private void Awake()
    {
        // Tạo carousel controller
        carouselController = gameObject.AddComponent<ProfileCarouselController>();

        LoadProfiles();
        InitializeCarousel();

        // Subscribe to controller events sau khi setup
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        if (carouselController != null)
        {
            // Subscribe to controller events
            carouselController.OnItemSelected.AddListener(OnProfileSelected);
            carouselController.OnCurrentItemUpdated.AddListener(OnProfileUpdated);
            carouselController.OnPrev.AddListener(OnPreviousPressed);
            carouselController.OnNext.AddListener(OnNextPressed);
        }
    }

    private void UnsubscribeFromEvents()
    {
        // if (carouselController != null)
        // {
        //     carouselController.OnItemSelected.RemoveListener(OnProfileSelected);
        //     carouselController.OnCurrentItemUpdated.RemoveListener(OnProfileUpdated);
        //     carouselController.OnPrev.RemoveListener(OnPreviousPressed);
        //     carouselController.OnNext.RemoveListener(OnNextPressed);
        // }
    }

    private void OnProfileSelected(ProfileData profile)
    {
        Debug.Log($"Profile Selected: {profile.playerId} - {profile.playerName}");
        OnItemSelected.Invoke(profile);
    }

    private void OnProfileUpdated(ProfileData profile)
    {
        Debug.Log($"Current Profile Updated: {profile.playerId} - {profile.playerName}");
        OnCurrentItemUpdated.Invoke(profile);
    }

    private void OnPreviousPressed()
    {
        Debug.Log("Previous button pressed");
        OnPrev.Invoke();

        // Auto select current item after navigation
        if (carouselController != null)
        {
            carouselController.SelectCurrent();
        }
    }

    private void OnNextPressed()
    {
        Debug.Log("Next button pressed");
        OnNext.Invoke();

        // Auto select current item after navigation
        if (carouselController != null)
        {
            carouselController.SelectCurrent();
        }
    }

    private void LoadProfiles()
    {
        if (ProfileManager.Instance == null)
        {
            Debug.LogWarning("ProfileManager.Instance is null");
            return;
        }

        Debug.Log($"ProfileCarousel LoadProfiles: {ProfileManager.Instance.profileList.profiles}");
        List<ProfileMeta> profileMetas = ProfileManager.Instance.profileList.profiles;

        profiles.Clear();
        for (int i = 0; i < profileMetas.Count; i++)
        {
            profiles.Add(new ProfileData
            {
                playerId = profileMetas[i].playerId,
                playerName = profileMetas[i].name,
                sprite = defaultProfileSprite
            });
        }
    }

    private void InitializeCarousel()
    {
        if (carouselController != null && profiles.Count > 0)
        {
            carouselController.Setup(profiles.ToArray(), carouselItemPrefab, isInfinity, childOrigin);
        }
    }

    // Public methods for UI buttons
    public void Next()
    {
        if (carouselController != null)
        {
            carouselController.Next();
        }
    }

    public void Previous()
    {
        if (carouselController != null)
        {
            carouselController.Previous();
        }
    }

    public void Select()
    {
        Debug.Log("Manual Select Called");
        if (carouselController != null)
        {
            carouselController.Select();
            PlayerManager.Instance.LoadForProfile(GlobalData.Instance.playerId, GlobalData.Instance.playerName);
        }
    }

    public void RemoveProfile(string playerId)
    {
        int index = profiles.FindIndex(p => p.playerId == playerId);
        if (index >= 0)
        {
            profiles.RemoveAt(index);
        }
    }

    public void RefreshCarousel()
    {
        // Unsubscribe from old events
        UnsubscribeFromEvents();

        // Kill all tweens
        DG.Tweening.DOTween.KillAll();

        // Destroy old controller
        if (carouselController != null)
        {
            Destroy(carouselController);
        }

        // Clear children
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Reload data
        LoadProfiles();

        // Create new controller
        carouselController = gameObject.AddComponent<ProfileCarouselController>();
        carouselController.Setup(data: profiles.ToArray(), itemPrefab: carouselItemPrefab, infinity: isInfinity, origin: childOrigin);

        // Re-subscribe to events
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }
}