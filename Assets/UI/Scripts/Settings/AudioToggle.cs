using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [Header("Звук")]
    public Button soundButton;      // Кнопка звука
    public TMP_Text soundButtonText; // Текст на кнопке звука

    [Header("Музыка")]
    public Button musicButton;      // Кнопка музыки
    public TMP_Text musicButtonText; // Текст на кнопке музыки

    private Color activeColor = Color.white;
    private Color inactiveColor;

    private void Start()
    {
        ColorUtility.TryParseHtmlString("#9C9C9C", out inactiveColor);

        // Добавляем обработчики нажатий
        soundButton.onClick.AddListener(ToggleSound);
        musicButton.onClick.AddListener(ToggleMusic);

        // Восстанавливаем состояние из PlayerPrefs
        UpdateSoundUI(PlayerPrefs.GetInt("SoundOn", 1) == 1);
        UpdateMusicUI(PlayerPrefs.GetInt("MusicOn", 1) == 1);
    }

    // ===== ЗВУК =====
    private void ToggleSound()
    {
        bool isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        isSoundOn = !isSoundOn; // Переключаем состояние
        PlayerPrefs.SetInt("SoundOn", isSoundOn ? 1 : 0);
        UpdateSoundUI(isSoundOn);
    }

    private void UpdateSoundUI(bool isOn)
    {
        soundButtonText.text = isOn ? "вкл" : "выкл";
        soundButtonText.color = isOn ? activeColor : inactiveColor;
    }

    // ===== МУЗЫКА =====
    private void ToggleMusic()
    {
        bool isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        isMusicOn = !isMusicOn; // Переключаем состояние
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        UpdateMusicUI(isMusicOn);
    }

    private void UpdateMusicUI(bool isOn)
    {
        musicButtonText.text = isOn ? "вкл" : "выкл";
        musicButtonText.color = isOn ? activeColor : inactiveColor;
    }
}
