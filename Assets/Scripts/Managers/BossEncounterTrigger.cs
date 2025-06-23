using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

/// <summary>
/// Trigger that activates the boss encounter when player enters
/// Handles the boss announcement sequence
/// </summary>
public class BossEncounterTrigger : MonoBehaviour
{
  [Header("Boss Encounter Settings")]
  [SerializeField] private GameObject bossGameObject;
  [SerializeField] private float encounterDuration = 3f;

  [Header("Boss Announcement UI")]
  [SerializeField] private Image bossAnnouncementBackground;
  [SerializeField] private TextMeshProUGUI bossAnnouncementText;

  [Header("Audio")]
  [SerializeField] private AudioSource musicAudioSource;
  [SerializeField] private AudioClip bossMusic;

  private bool hasTriggered = false;
  private GameObject player;

  private void Awake()
  {
    player = GameObject.FindGameObjectWithTag("Player");

    if (bossAnnouncementBackground != null)
    {
      bossAnnouncementBackground.gameObject.SetActive(false);
    }
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (hasTriggered) return;

    if (other.CompareTag("Player"))
    {
      hasTriggered = true;
      StartCoroutine(BossEncounterSequence());
    }
  }

  private IEnumerator BossEncounterSequence()
  {
    FreezeGameplay();

    ShowBossAnnouncement();

    PlayBossMusic();

    yield return new WaitForSecondsRealtime(encounterDuration);

    HideBossAnnouncement();

    ActivateBoss();

    UnfreezeGameplay();

    gameObject.SetActive(false);
  }

  private void FreezeGameplay()
  {
    Time.timeScale = 0f;

    if (player != null)
    {
      Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
      if (playerRb != null)
      {
        playerRb.simulated = false;
      }
    }
  }

  private void UnfreezeGameplay()
  {
    Time.timeScale = 1f;

    if (player != null)
    {
      Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
      if (playerRb != null)
      {
        playerRb.simulated = true;
      }
    }
  }

  private void ShowBossAnnouncement()
  {
    if (bossAnnouncementBackground != null)
    {
      bossAnnouncementBackground.gameObject.SetActive(true);

      if (bossAnnouncementText != null)
      {
        bossAnnouncementText.text = "BOSS IS COMING HEHE!!!";
        bossAnnouncementText.gameObject.SetActive(true);
      }
    }
  }

  private void HideBossAnnouncement()
  {
    if (bossAnnouncementBackground != null)
    {
      bossAnnouncementBackground.gameObject.SetActive(false);

      if (bossAnnouncementText != null)
      {
        bossAnnouncementText.gameObject.SetActive(false);
      }
    }
  }

  private void PlayBossMusic()
  {
    if (musicAudioSource != null && bossMusic != null)
    {
      musicAudioSource.clip = bossMusic;
      musicAudioSource.Play();
    }
  }

  private void ActivateBoss()
  {
    if (bossGameObject != null)
    {
      bossGameObject.SetActive(true);

      if (GameManager.Instance != null)
      {
        GameManager.Instance.OnBossSpawned(bossGameObject);
      }
    }
  }
}
