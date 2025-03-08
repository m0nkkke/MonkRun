using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
