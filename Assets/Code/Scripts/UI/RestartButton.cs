using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    // This function reloads the active scene, effectively restarting the game.
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.StartGame();
        Time.timeScale = 1f; // Ensure time scale is reset to normal
    }
}
