using UnityEngine;

/// <summary>
/// Simple test script for the complete high score system.
/// Add this to a GameObject in your scene to test everything.
/// </summary>
public class HighScoreSystemTester : MonoBehaviour
{
    [Header("Test Settings")]
    [SerializeField] private int testScoreIncrement = 100;
    [SerializeField] private string[] testPlayerNames = { "Alice", "Bob", "Charlie", "Diana", "Eve" };

    private int currentTestScore = 500;
    private int nameIndex = 0;

    private void Start()
    {
        Debug.Log("=== High Score System Tester Started ===");
        Debug.Log("Controls:");
        Debug.Log("T - Add test score");
        Debug.Log("H - Show high scores");
        Debug.Log("C - Clear high scores");
        Debug.Log("G - Simulate game over");
        Debug.Log("R - Restart game");
        Debug.Log("1-5 - Add specific test scores");
    }

    private void Update()
    {
        // Test adding scores
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddTestScore();
        }

        // Show high scores
        if (Input.GetKeyDown(KeyCode.H))
        {
            ShowHighScores();
        }

        // Clear high scores
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearHighScores();
        }

        // Simulate game over
        if (Input.GetKeyDown(KeyCode.G))
        {
            SimulateGameOver();
        }

        // Restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        // Add specific test scores
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddSpecificScore(750, "TestPlayer1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddSpecificScore(1200, "TestPlayer2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddSpecificScore(950, "TestPlayer3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AddSpecificScore(1500, "TestPlayer4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddSpecificScore(2000, "TestPlayer5");
        }
    }

    private void AddTestScore()
    {
        if (HighScoreManager.Instance == null)
        {
            Debug.LogError("HighScoreManager not found!");
            return;
        }

        string playerName = testPlayerNames[nameIndex % testPlayerNames.Length];
        bool wasAdded = HighScoreManager.Instance.AddScore(currentTestScore, playerName);

        Debug.Log($"Added score {currentTestScore} for {playerName}. High score: {wasAdded}");

        currentTestScore += Random.Range(50, testScoreIncrement * 2);
        nameIndex++;
    }

    private void AddSpecificScore(int score, string playerName)
    {
        if (HighScoreManager.Instance == null)
        {
            Debug.LogError("HighScoreManager not found!");
            return;
        }

        bool wasAdded = HighScoreManager.Instance.AddScore(score, playerName);
        Debug.Log($"Added specific score {score} for {playerName}. High score: {wasAdded}");
    }

    private void ShowHighScores()
    {
        if (HighScoreManager.Instance == null)
        {
            Debug.LogError("HighScoreManager not found!");
            return;
        }

        var scores = HighScoreManager.Instance.GetFormattedHighScores();
        Debug.Log("=== CURRENT HIGH SCORES ===");

        if (scores.Count == 0)
        {
            Debug.Log("No high scores yet!");
        }
        else
        {
            foreach (var score in scores)
            {
                Debug.Log(score);
            }
            Debug.Log($"Highest Score: {HighScoreManager.Instance.HighestScore:N0}");
        }
    }

    private void ClearHighScores()
    {
        if (HighScoreManager.Instance == null)
        {
            Debug.LogError("HighScoreManager not found!");
            return;
        }

        HighScoreManager.Instance.ClearHighScores();
        Debug.Log("High scores cleared!");
    }

    private void SimulateGameOver()
    {
        if (ScoreManager.Instance == null)
        {
            Debug.LogError("ScoreManager not found!");
            return;
        }

        // Add some points to current score
        ScoreManager.Instance.AddPoints(Random.Range(100, 500));

        // Trigger game over
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EndGame();
        }
        else
        {
            Debug.Log("GameManager not found, handling game over manually...");

            if (HighScoreManager.Instance != null)
            {
                int finalScore = ScoreManager.Instance.Score;
                bool isHighScore = HighScoreManager.Instance.AddScore(finalScore, "TestPlayer");

                if (isHighScore)
                {
                    Debug.Log($"🎉 NEW HIGH SCORE! {finalScore}");
                }
                else
                {
                    Debug.Log($"Game Over. Final Score: {finalScore}");
                }
            }
        }
    }

    private void RestartGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.ResetScore();
            }
            Debug.Log("Game restarted (score reset)");
        }
    }
}
