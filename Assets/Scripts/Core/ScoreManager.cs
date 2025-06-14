using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }
    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        print(this.name + " Awake called");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Score = 0;
    }

    public void AddPoints(int points)
    {
        Score += points;

        print($"ScoreManager: Added {points} points. New score: {Score}");
        OnScoreChanged?.Invoke(Score);
    }

    public void DeductPoints(int points)
    {
        Score -= points;
        if (Score < 0) Score = 0;
        OnScoreChanged?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }
}
