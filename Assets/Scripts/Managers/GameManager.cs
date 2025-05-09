using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance_GameManager { get; private set; }
    private void Start()
    {
        FindAudioSource();
    }
    private void Update()
    {

    }

    [System.Serializable]
    public class AudioData
    {
        public AudioSource musicSource;
        public GameObject musicObject;

        public AudioSource sfxSource;
        public GameObject sfxObject;

        public float volumeSfx;
        public float volumeMusic;
        public void Play(AudioClip chosenClip)
        {

        }
    }

    public AudioData[] audioDataArray;

    public AudioSource[] audioSources;

    private void Awake()
    {
        if (Instance_GameManager == null)
        {
            Instance_GameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayAudio()
    {
        
    }

    public void FindAudioSource()
    {
        var objs = GameObject.FindObjectsOfType<AudioSource>();
        audioDataArray = new AudioData[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            var temp = new AudioData();
            temp.sfxSource = objs[i];
            audioDataArray = temp;
        }
    }
}
