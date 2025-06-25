using UnityEngine;

public class GlobalData : MonoBehaviour
{
    private static GlobalData instance;

    public static GlobalData Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singletonObject = new GameObject("GlobalData");
                instance = singletonObject.AddComponent<GlobalData>();
                singletonObject.name = typeof(GlobalData).ToString();
                DontDestroyOnLoad(singletonObject);
            }
            return instance;
        }
    }

    public int playerScore;
    public string playerName;
    public string playerId;
}
