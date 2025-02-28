using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsScript : MonoBehaviour
{
    public AudioClip[] sounds;

    private AudioSource audioSrc => GetComponent<AudioSource>();

    public void PlaySound(AudioClip clip, float volume = 1f, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        audioSrc.pitch = p1;
        audioSrc.PlayOneShot(clip, volume);
    }

}
