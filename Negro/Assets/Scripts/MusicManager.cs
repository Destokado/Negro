using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    [SerializeField] private AudioClip[] music;
    private int currentMusicIndex = -1;
    private AudioSource audioSource;

    private static MusicManager instance;
    
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayAllSongs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayAllSongs();
        }
    }

    private void PlayAllSongs()
    {
        CancelInvoke(nameof(PlayAllSongs));
        
        currentMusicIndex++;
        if (currentMusicIndex > music.Length - 1)
            currentMusicIndex = 0;
        
        audioSource.clip = music[currentMusicIndex];
        audioSource.Play();
        
        Invoke(nameof(PlayAllSongs), audioSource.clip.length);
    }
}
