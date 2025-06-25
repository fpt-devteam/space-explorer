using UnityEngine;

public class InGameSound : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.inGameMusic);
    }
}
