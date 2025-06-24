using UnityEngine;

public class MenuSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.mainMenuMusic);
    }
}
