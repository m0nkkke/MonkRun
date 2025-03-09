using System;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Audio;

public class GameSoundManager : MonoBehaviour
{
    public static GameSoundManager Instance;

    public AudioSource audioSource;

    public AudioClip jumpSound;
    public AudioClip crouchSound;
    public AudioClip hitSound;
    public AudioClip bananaSound;
    public AudioClip mushroomSound;

    public AudioSource backgroundMusicSource;

    public event Action OnJump;
    public event Action OnCrouch;
    public event Action OnHit;
    public event Action OnBanana;
    public event Action OnMushroom;

    public AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        PlayBackgroundMusic();
    }
    private void OnEnable()
    {
        OnJump += PlayJumpSound;
        OnCrouch += PlaySlideSound;
        OnHit += PlayHitSound;
        OnBanana += PlayBananaSound;
        OnMushroom += PlayMushroomSound;
    }

    private void OnDisable()
    {
        OnJump -= PlayJumpSound;
        OnCrouch -= PlaySlideSound;
        OnHit -= PlayHitSound;
        OnBanana -= PlayBananaSound;
        OnMushroom -= PlayMushroomSound;
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

    private void PlayMushroomSound()
    {
        audioSource.PlayOneShot(mushroomSound); 
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

    public void TriggerMushroomSound()
    {
        OnMushroom?.Invoke();
    }

    // Воспроизведение фоновой музыки
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true; 
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

        PlayerPrefs.SetInt("BackgroundMusicEnabled", isEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        bool musicEnabled = PlayerPrefs.GetInt("BackgroundMusicEnabled", 1) == 1;
        SetBackgroundMusicEnabled(musicEnabled);
    }
}
