using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public static PlayerManager Instance { get; private set; }
  public PlayerData playerData;
  void Awake()
  {
    if (Instance != null && Instance != this) { Destroy(gameObject); return; }
    Instance = this; DontDestroyOnLoad(gameObject);
  }

  public void PurchaseItem(string itemId)
  {
    if (!playerData.ownedItemIds.Contains(itemId))
    {
      playerData.ownedItemIds.Add(itemId);
      Save();
    }
  }

  public void UpdateHighScore(int newScore)
  {
    if (newScore > playerData.highScore)
    {
      playerData.highScore = newScore;
      Save();
    }
  }

  public void UpdateLevel(int newLevel)
  {
    playerData.level = newLevel;
    Save();
  }
  public void LoadForProfile(string profileId)
  {
    string fileName = $"player_{profileId}.json";
    playerData = LocalDataService.Instance.Load<PlayerData>(fileName);
    Debug.Log($"player Id 31: {playerData.playerId}");
    if (string.IsNullOrEmpty(playerData.playerId))
    {
      playerData.playerId = profileId;
      playerData.playerName = ProfileManager.Instance.currentProfile.name;
      playerData.highScore = 0;
      playerData.avatarImagePath = ProfileManager.Instance.currentProfile.avatarImagePath;
      playerData.numStars = 0;
      playerData.level = 1;
      playerData.ownedItemIds = new List<string>();
      Save();
    }
  }

  public void Save()
  {
    string fileName = $"player_{playerData.playerId}.json";
    LocalDataService.Instance.Save(playerData, fileName);
  }
}
