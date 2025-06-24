using System;
using System.IO;
using UnityEngine;

public class LocalDataService : MonoBehaviour
{
    public static LocalDataService Instance { get; private set; }
    string dataPath => Application.persistentDataPath + "/";

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Save<T>(T data, string fileName)
    {
        string json = JsonUtility.ToJson(obj: data, prettyPrint: true);
        File.WriteAllText(path: Path.Combine(path1: dataPath, path2: fileName), contents: json);
    }

    public T Load<T>(string fileName) where T : new()
    {
        string fullPath = Path.Combine(path1: dataPath, path2: fileName);
        Debug.Log($"Loading data from: {fullPath}");
        if (!File.Exists(path: fullPath)) return new T();
        string json = File.ReadAllText(path: fullPath);
        return JsonUtility.FromJson<T>(json: json);
    }
}
