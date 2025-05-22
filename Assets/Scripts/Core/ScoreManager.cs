using UnityEngine;
using System;

/// <summary>
/// Manages the player's score and notifies listeners of changes.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }
    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Score = 0;
    }

    /// <summary>
    /// Adds points to the score and notifies listeners.
    /// </summary>
    public void AddPoints(int points)
    {
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    /// <summary>
    /// Deducts points from the score and notifies listeners.
    /// </summary>
    public void DeductPoints(int points)
    {
        Score -= points;
        if (Score < 0) Score = 0;
        OnScoreChanged?.Invoke(Score);
    }

    /// <summary>
    /// Resets the score to zero and notifies listeners.
    /// </summary>
    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }
}
