using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays the high score leaderboard.
/// </summary>
public class HighScoreDisplayUI : MonoBehaviour
{
    [SerializeField] private Transform scoreEntryParent;
    [SerializeField] private GameObject scoreEntryPrefab;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private int maxDisplayEntries = 10;

    private readonly List<GameObject> scoreEntryObjects = new List<GameObject>();

    private void Start()
    {
        if (titleText != null)
            titleText.text = "HIGH SCORES";

        UpdateDisplay();
    }

    private void OnEnable()
    {
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.OnHighScoresUpdated += OnHighScoresUpdated;
        }
        UpdateDisplay();
    }

    private void OnDisable()
    {
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.OnHighScoresUpdated -= OnHighScoresUpdated;
        }
    }

    private void OnHighScoresUpdated(List<HighScoreEntry> updatedScores)
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        ClearDisplay();

        if (HighScoreManager.Instance == null) return;

        var highScores = HighScoreManager.Instance.HighScores;
        int entriesToShow = Mathf.Min(highScores.Count, maxDisplayEntries);

        for (int i = 0; i < entriesToShow; i++)
        {
            CreateScoreEntry(i + 1, highScores[i]);
        }

        // Show "No high scores yet" if empty
        if (highScores.Count == 0)
        {
            CreateEmptyEntry();
        }
    }

    private void CreateScoreEntry(int rank, HighScoreEntry entry)
    {
        GameObject entryObj = null;

        // Use prefab if available, otherwise create simple text
        if (scoreEntryPrefab != null && scoreEntryParent != null)
        {
            entryObj = Instantiate(scoreEntryPrefab, scoreEntryParent);
        }
        else
        {
            entryObj = new GameObject($"ScoreEntry_{rank}");
            entryObj.transform.SetParent(scoreEntryParent != null ? scoreEntryParent : transform);
        }

        // Try to find TextMeshProUGUI component
        var textComponent = entryObj.GetComponent<TextMeshProUGUI>();
        if (textComponent == null)
        {
            textComponent = entryObj.GetComponentInChildren<TextMeshProUGUI>();
        }

        // If no TextMeshProUGUI, try regular Text
        if (textComponent == null)
        {
            var regularText = entryObj.GetComponent<UnityEngine.UI.Text>();
            if (regularText == null)
            {
                regularText = entryObj.GetComponentInChildren<UnityEngine.UI.Text>();
            }
            if (regularText != null)
            {
                regularText.text = FormatScoreEntry(rank, entry);
            }
        }
        else
        {
            textComponent.text = FormatScoreEntry(rank, entry);
        }

        scoreEntryObjects.Add(entryObj);
    }

    private void CreateEmptyEntry()
    {
        GameObject entryObj = new GameObject("EmptyEntry");
        entryObj.transform.SetParent(scoreEntryParent != null ? scoreEntryParent : transform);

        var textComponent = entryObj.AddComponent<TextMeshProUGUI>();
        textComponent.text = "No high scores yet!";
        textComponent.fontSize = 18;
        textComponent.color = Color.gray;
        textComponent.alignment = TextAlignmentOptions.Center;

        scoreEntryObjects.Add(entryObj);
    }

    private string FormatScoreEntry(int rank, HighScoreEntry entry)
    {
        return $"{rank}. {entry.playerName} - {entry.score:N0}";
    }

    private void ClearDisplay()
    {
        foreach (var obj in scoreEntryObjects)
        {
            if (obj != null)
                DestroyImmediate(obj);
        }
        scoreEntryObjects.Clear();
    }

    public void OnClearHighScoresButton()
    {
        if (HighScoreManager.Instance != null)
        {
            HighScoreManager.Instance.ClearHighScores();
        }
    }
}