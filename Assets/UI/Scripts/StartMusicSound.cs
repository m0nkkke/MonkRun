using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StartMusicSound : MonoBehaviour
{
    public AudioMixerGroup MusicMixer;
    public AudioMixerGroup SoundsMixer;
    void Start()
    {
        bool isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        if (!isMusicOn) MusicMixer.audioMixer.SetFloat("musicVolume", -80);

        bool isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        if (!isSoundOn) MusicMixer.audioMixer.SetFloat("soundsVolume", -80);
    }

}
