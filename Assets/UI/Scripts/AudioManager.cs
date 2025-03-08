using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    //public void Start()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}
}
