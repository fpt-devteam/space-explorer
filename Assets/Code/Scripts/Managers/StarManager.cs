using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class StarManager : MonoBehaviour
{
  public static StarManager Instance { get; private set; }

  [Header("UI")]
  [SerializeField] private TextMeshProUGUI StarText;

  private int Star = 0;

  private void LoadStar()
  {
    // if (PlayerManager.Instance != null)
    // {
    //   Star = PlayerManager.Instance.playerData.numStars;
    //   Debug.Log($"StarManager: Loaded Star: {Star}");
    // }
    // else
    // {
    //   Debug.LogWarning("StarManager: PlayerManager instance is null. Cannot load stars.");
    // }
  }

  private void Awake()
  {
    Debug.Log("StarManager: Awake called");
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    LoadStar();
    UpdateUI();
  }

  public void AddPoints(int points)
  {

    Star += points;
    UpdateUI();
    SaveData();
    Debug.Log($"StarManager: Added {points} points. New Star: {Star}");
  }

  public void SaveData()
  {
    // PlayerManager.Instance.UpdateStarCount(Star);
    // Debug.Log($"StarManager: Saved Star: {Star}");
  }

  public void ResetStar()
  {
    Star = 0;
    UpdateUI();
  }

  private void UpdateUI()
  {
    if (StarText != null)
      StarText.text = $"Star: {Star}";
  }
}