using System;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public static GameSoundManager Instance;

    public AudioSource audioSource;

    public AudioClip jumpSound;
    public AudioClip crouchSound;
    public AudioClip hitSound;
    public AudioClip bananaSound;

    public AudioSource backgroundMusicSource;

    public event Action OnJump;
    public event Action OnCrouch;
    public event Action OnHit;
    public event Action OnBanana;

    public AudioClip backgroundMusic; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Включаем фоновую музыку при старте игры
        PlayBackgroundMusic();
    }
    private void OnEnable()
    {
        OnJump += PlayJumpSound;
        OnCrouch += PlaySlideSound;
        OnHit += PlayHitSound;
        OnBanana += PlayBananaSound;
    }

    private void OnDisable()
    {
        OnJump -= PlayJumpSound;
        OnCrouch -= PlaySlideSound;
        OnHit -= PlayHitSound;
        OnBanana -= PlayBananaSound;
    }

    private void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    private void PlaySlideSound()
    {
        audioSource.PlayOneShot(crouchSound);
    }

    private void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
    private void PlayBananaSound()
    {
        audioSource.PlayOneShot(bananaSound);
    }


    public void TriggerJumpSound()
    {
        OnJump?.Invoke(); // Вызов события прыжка
    }

    public void TriggerCrouchSound()
    {
        OnCrouch?.Invoke(); // Вызов события приседания
    }
    public void TriggerHitSound()
    {
        OnHit?.Invoke(); // Вызов события удара
    }

    public void TriggerBananaSound()
    {
        OnBanana?.Invoke();
    }

    // Воспроизведение фоновой музыки
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true; // Зацикливаем музыку
            backgroundMusicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Stop();
        }
    }

    // Включение/отключение фоновой музыки через настройки
    public void SetBackgroundMusicEnabled(bool isEnabled)
    {
        if (isEnabled)
        {
            PlayBackgroundMusic();
        }
        else
        {
            StopBackgroundMusic();
        }

        // Сохраняем состояние в настройках
        PlayerPrefs.SetInt("BackgroundMusicEnabled", isEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Проверка настроек при запуске
    private void LoadSettings()
    {
        bool musicEnabled = PlayerPrefs.GetInt("BackgroundMusicEnabled", 1) == 1;
        SetBackgroundMusicEnabled(musicEnabled);
    }
}
