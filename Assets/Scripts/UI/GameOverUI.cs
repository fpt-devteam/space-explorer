using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        print("Game Over! Displaying Game Over UI.");
        gameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
