using System;
using System.Collections.Generic;
using System.Linq;
using HasanSadikin.Carousel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ProfileCarouselController : CarouselController<ProfileData>
{
    private void OnEnable()
    {
        // Subscribe to events for debugging
        OnItemSelected.AddListener(LogSelectedItem);
        OnCurrentItemUpdated.AddListener(LogCurrentItem);
    }

    private void OnDisable()
    {
        // Unsubscribe from events
        // OnItemSelected.RemoveListener(LogSelectedItem);
        // OnCurrentItemUpdated.RemoveListener(LogCurrentItem);
    }

    private void LogSelectedItem(ProfileData data)
    {
        Debug.Log($"[Controller] Selected Profile: {data.playerId} - {data.playerName}");
    }

    private void LogCurrentItem(ProfileData data)
    {
        Debug.Log($"[Controller] Current Profile: {data.playerId} - {data.playerName}");
    }

    public void Setup(ProfileData[] data, ProfileCarouselItem itemPrefab, bool infinity, Origin origin)
    {
        Debug.Log($"Setting up carousel with {data.Length} profiles");

        _data = data;
        _carouselItemPrefab = itemPrefab;
        _isInfinity = infinity;
        _childOrigin = origin;

        // Initialize the carousel
        Start();
    }

    public void RefreshData(ProfileData[] data)
    {
        Debug.Log($"Refreshing carousel with {data.Length} profiles");
        _data = data;
        Start();
    }

    // Method to select current item (useful for auto-select after navigation)
    public void SelectCurrent()
    {
        if (_data != null && _data.Length > 0 && _currentIndex >= 0 && _currentIndex < _data.Length)
        {
            Debug.Log($"Auto-selecting current profile at index {_currentIndex}");
            Select();
        }
    }

    // Override Next method to add debug and auto-select
    public override void Next()
    {
        base.Next();
        Debug.Log($"Moved to next profile. Current index: {_currentIndex}");
        SelectCurrent();
    }

    // Override Previous method to add debug and auto-select
    public override void Previous()
    {
        base.Previous();
        Debug.Log($"Moved to previous profile. Current index: {_currentIndex}");
        SelectCurrent();
    }

    // Override Select method to add debug
    public override void Select()
    {
        if (_data != null && _data.Length > 0 && _currentIndex >= 0 && _currentIndex < _data.Length)
        {
            Debug.Log($"Selecting profile at index {_currentIndex}: {_data[_currentIndex].playerName} : {_data[_currentIndex].playerId}");
            GlobalData.Instance.playerId = _data[_currentIndex].playerId;
            GlobalData.Instance.playerName = _data[_currentIndex].playerName;
            base.Select();
        }
        else
        {
            Debug.LogWarning("Cannot select: Invalid data or index");
        }
    }
}