using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance_AudioManager { get; private set; }
    public AudioSource[] audioSource;
    public AudioClip[] musicClip;
    public AudioClip[] sfxClip;
    private void Awake()
    {
        if (Instance_AudioManager == null)
        {
            Instance_AudioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }
    private void Update()
    {
    }
    public void PlaySFX()
    {
        /*
        GetInstanceID();
        audioSource[0]
        */
    }
}
