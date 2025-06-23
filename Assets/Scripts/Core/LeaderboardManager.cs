using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }
    public LeaderboardData leaderboardData;
    private string leaderboardFileName = "leaderboard.json";
    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this; DontDestroyOnLoad(gameObject);
    }

    public void LoadLeaderboardData()
    {
        leaderboardData = LocalDataService.Instance.Load<LeaderboardData>(leaderboardFileName);
    }

    public void SaveLeaderboardData()
    {
        LocalDataService.Instance.Save(leaderboardData, leaderboardFileName);
    }

    public void AddScore(LeaderboardEntry newScore)
    {
        leaderboardData.topEntries.Add(newScore);
        leaderboardData.topEntries.Sort((a, b) => b.score.CompareTo(a.score));
        leaderboardData.topEntries = leaderboardData.topEntries.GetRange(0, Math.Min(10, leaderboardData.topEntries.Count));
        SaveLeaderboardData();
    }
}
