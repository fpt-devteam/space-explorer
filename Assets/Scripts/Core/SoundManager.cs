using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager Instance { get; private set; }
   [Header("Audio Sources")]
   [SerializeField] private AudioSource _musicSource;
   [SerializeField] private AudioSource _sfxSource;

   [Header("Music")]
   public AudioClip mainMenuMusic;
   public AudioClip inGameMusic;
   public AudioClip bossFightMusic;

   [Header("SFX - Enemy")]
   public AudioClip bossLaugh;
   public AudioClip bossShot;

   [Header("SFX - Event")]
   public AudioClip bloodLow;
   public AudioClip fallingCoin;
   public AudioClip gameOver;
   public AudioClip hitLava;
   public AudioClip hitStar;
   public AudioClip hitShield;
   public AudioClip hitAsteroid;
   public AudioClip victory;
   public AudioClip levelUp;
   public AudioClip onClickSelectMenu;
   public AudioClip boomAsteroid;
   public AudioClip shotBoss;
   public AudioClip boomSpaceShip;

   [Header("SFX - Player")]
   public AudioClip laserShot;
   public AudioClip playerThrust;
   public AudioClip playerDrift;
   public AudioClip playerProjectile;
   public AudioClip playerBoost;

   [Header("UI")]

   public AudioClip buttonClick;

   private void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(gameObject);
         return;
      }
      Instance = this;
      DontDestroyOnLoad(gameObject);
   }

   public void PlayMusic(AudioClip clip)
   {
      if (_musicSource.isPlaying)
      {
         _musicSource.Stop();
      }
      if (clip != null)
      {
         _musicSource.clip = clip;
         _musicSource.Play();
      }
   }

   public void PlaySFX(AudioClip clip)
   {
      if (clip != null)
      {
         _sfxSource.PlayOneShot(clip);
      }
   }

   public void PlayButtonClick()
   {
      PlaySFX(buttonClick);
   }
}
