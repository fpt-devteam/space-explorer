using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages high scores with JSON file persistence.
/// </summary>
public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }

    [SerializeField] private int maxHighScores = 10;
    [SerializeField] private string defaultPlayerName = "Anonymous";

    private HighScoreData highScoreData;
    private string saveFilePath;

    // Events
    public event Action<List<HighScoreEntry>> OnHighScoresUpdated;

    // Properties
    public List<HighScoreEntry> HighScores => highScoreData?.highScores ?? new List<HighScoreEntry>();
    public int HighestScore => HighScores.Count > 0 ? HighScores[0].score : 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Set save file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "highscores.json");

        // Load existing high scores
        LoadHighScores();
    }

    /// <summary>
    /// Adds a new score to the high score list if it qualifies.
    /// </summary>
    /// <param name="score">The score to add</param>
    /// <param name="playerName">The player's name (optional)</param>
    /// <returns>True if the score was added to the high score list</returns>
    public bool AddScore(int score, string playerName = null)
    {
        if (string.IsNullOrEmpty(playerName))
            playerName = defaultPlayerName;

        string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        HighScoreEntry newEntry = new HighScoreEntry(score, playerName, currentDate);

        // Add the new score
        highScoreData.highScores.Add(newEntry);

        // Sort by score (descending)
        highScoreData.highScores = highScoreData.highScores
            .OrderByDescending(entry => entry.score)
            .Take(maxHighScores)
            .ToList();

        // Check if the new score made it into the top scores
        bool scoreAdded = highScoreData.highScores.Contains(newEntry);

        if (scoreAdded)
        {
            SaveHighScores();
            OnHighScoresUpdated?.Invoke(HighScores);
            Debug.Log($"New high score added: {score} by {playerName}");
        }

        return scoreAdded;
    }

    /// <summary>
    /// Checks if a score qualifies as a high score.
    /// </summary>
    /// <param name="score">The score to check</param>
    /// <returns>True if the score would make it into the high score list</returns>
    public bool IsHighScore(int score)
    {
        if (highScoreData.highScores.Count < maxHighScores)
            return true;

        return score > highScoreData.highScores.Last().score;
    }

    /// <summary>
    /// Gets the rank of a score (1-based, 0 if not in high scores).
    /// </summary>
    /// <param name="score">The score to check</param>
    /// <returns>The rank of the score, or 0 if not in high scores</returns>
    public int GetScoreRank(int score)
    {
        for (int i = 0; i < highScoreData.highScores.Count; i++)
        {
            if (score >= highScoreData.highScores[i].score)
                return i + 1;
        }

        return highScoreData.highScores.Count < maxHighScores ? highScoreData.highScores.Count + 1 : 0;
    }

    /// <summary>
    /// Clears all high scores.
    /// </summary>
    public void ClearHighScores()
    {
        highScoreData.highScores.Clear();
        SaveHighScores();
        OnHighScoresUpdated?.Invoke(HighScores);
        Debug.Log("High scores cleared");
    }

    /// <summary>
    /// Saves high scores to JSON file.
    /// </summary>
    private void SaveHighScores()
    {
        try
        {
            string jsonData = JsonUtility.ToJson(highScoreData, true);
            File.WriteAllText(saveFilePath, jsonData);
            Debug.Log($"High scores saved to: {saveFilePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save high scores: {e.Message}");
        }
    }

    /// <summary>
    /// Loads high scores from JSON file.
    /// </summary>
    private void LoadHighScores()
    {
        try
        {
            if (File.Exists(saveFilePath))
            {
                string jsonData = File.ReadAllText(saveFilePath);
                highScoreData = JsonUtility.FromJson<HighScoreData>(jsonData);

                // Ensure the list is sorted
                highScoreData.highScores = highScoreData.highScores
                    .OrderByDescending(entry => entry.score)
                    .Take(maxHighScores)
                    .ToList();

                Debug.Log($"High scores loaded from: {saveFilePath}");
                OnHighScoresUpdated?.Invoke(HighScores);
            }
            else
            {
                // Create new high score data if file doesn't exist
                highScoreData = new HighScoreData();
                Debug.Log("No existing high score file found. Created new high score data.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load high scores: {e.Message}");
            highScoreData = new HighScoreData(); // Fallback to empty data
        }
    }

    /// <summary>
    /// Gets high scores as formatted strings for display.
    /// </summary>
    /// <returns>List of formatted high score strings</returns>
    public List<string> GetFormattedHighScores()
    {
        List<string> formattedScores = new List<string>();

        for (int i = 0; i < highScoreData.highScores.Count; i++)
        {
            var entry = highScoreData.highScores[i];
            formattedScores.Add($"{i + 1}. {entry.playerName} - {entry.score:N0} ({entry.date})");
        }

        return formattedScores;
    }
}
