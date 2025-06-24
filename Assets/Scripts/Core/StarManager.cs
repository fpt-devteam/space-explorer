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

  private void Awake()
  {
    Debug.Log("StarManager: Awake called");
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    Star = PlayerManager.Instance.playerData.numStars;
    UpdateUI();
  }

  public void AddPoints(int points)
  {

    Star += points;
    PlayerManager.Instance.UpdateStarCount(Star);
    UpdateUI();

    Debug.Log($"StarManager: Added {points} points. New Star: {Star}");
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