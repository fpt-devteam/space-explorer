using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene transitions in the game.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Loads a scene by its name.
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Loads a scene by its build index.
    /// </summary>
    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
