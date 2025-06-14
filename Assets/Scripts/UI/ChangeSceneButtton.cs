using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButtton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeScene(string sceneName)
    {
        print("Changing scene to: " + sceneName);

        if (sceneName == "SampleScene") GameManager.Instance.StartGame();
        SceneManager.LoadScene(sceneName);
    }
}
