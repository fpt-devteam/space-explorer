using System;
using System.Collections.Generic;

/// <summary>
/// Data structure for storing high scores.
/// </summary>
[Serializable]
public class HighScoreData
{
    public List<HighScoreEntry> highScores = new List<HighScoreEntry>();

    public HighScoreData()
    {
        highScores = new List<HighScoreEntry>();
    }
}

/// <summary>
/// Individual high score entry.
/// </summary>
[Serializable]
public class HighScoreEntry
{
    public int score;
    public string playerName;
    public string date;

    public HighScoreEntry(int score, string playerName, string date)
    {
        this.score = score;
        this.playerName = playerName;
        this.date = date;
    }
}
