using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButtton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void ChangeScene(string sceneName)
    {
        print("Changing scene to: " + sceneName);
        Debug.Log("Changing scene to: " + sceneName);

        if (sceneName == "SampleScene") GameManager.Instance.StartGame();
        SceneManager.LoadScene(sceneName);
    }
}
