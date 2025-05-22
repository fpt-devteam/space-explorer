using UnityEngine;

public enum GameState { MainMenu, Playing, GameOver }

/// <summary>
/// Manages the overall game state and transitions.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        CurrentState = GameState.MainMenu;
    }

    /// <summary>
    /// Starts the game and sets the state to Playing.
    /// </summary>
    public void StartGame()
    {
        CurrentState = GameState.Playing;
    }

    /// <summary>
    /// Ends the game and sets the state to GameOver.
    /// </summary>
    public void EndGame()
    {
        CurrentState = GameState.GameOver;
    }

    /// <summary>
    /// Returns to the main menu.
    /// </summary>
    public void ReturnToMenu()
    {
        CurrentState = GameState.MainMenu;
    }
}
