using UnityEngine;

public class PlayerLaserSound : MonoBehaviour
{
  void Start()
  {
    SoundManager.Instance.PlaySFX(SoundManager.Instance.laserShot);
  }
}
