using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;
public enum GameState { MainMenu, Playing, GameOver }

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }
  public GameState CurrentState { get; private set; }

  [Header("Boss UI")]
  [SerializeField] private Slider bossHealthSlider;

  [Header("Audio")]
  [SerializeField] private AudioSource musicAudioSource;
  [SerializeField] private AudioClip normalMusic;

  private GameObject currentBoss;

  // Events
  public event Action<int, bool> OnGameEnded;

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    DontDestroyOnLoad(gameObject);
    CurrentState = GameState.MainMenu;
  }

  private void Start()
  {
    if (bossHealthSlider != null)
    {
      bossHealthSlider.gameObject.SetActive(false);
    }
  }

  public void StartGame()
  {
    CurrentState = GameState.Playing;
    if (ScoreManager.Instance != null)
    {
      ScoreManager.Instance.ResetScore();
    }

    PlayNormalMusic();
  }

  public void OnBossSpawned(GameObject boss)
  {
    currentBoss = boss;

    Boss bossScript = boss.GetComponent<Boss>();
    if (bossScript != null && bossHealthSlider != null)
    {
      bossScript.healthSlider = bossHealthSlider;
      bossHealthSlider.gameObject.SetActive(true);
      bossHealthSlider.value = 1f;
    }
    boss.SetActive(true);
  }

  private void PlayNormalMusic()
  {
    if (musicAudioSource != null && normalMusic != null)
    {
      musicAudioSource.clip = normalMusic;
      musicAudioSource.Play();
    }
  }

  public void OnBossDefeated()
  {
    if (bossHealthSlider != null)
    {
      bossHealthSlider.gameObject.SetActive(false);
    }

    PlayNormalMusic();
    currentBoss = null;
  }

  public void EndGame()
  {
    CurrentState = GameState.GameOver;

    if (ScoreManager.Instance != null)
    {
      int finalScore = ScoreManager.Instance.Score;
      OnGameEnded?.Invoke(finalScore, false);
      Debug.Log($"Game Over. Final Score: {finalScore}");
    }
  }

  public void ReturnToMenu()
  {
    CurrentState = GameState.MainMenu;
  }

    public void RestartGame()
    {
        StartGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void SetPanelActive(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Panel is null, it.");
        }
    }
}
