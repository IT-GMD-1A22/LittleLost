using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(AudioSource))]
public class NarratorSoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioClip clip_1;

    public static NarratorSoundManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundClip(int ClipID)
    {
        _audioSource.Stop();

        switch (ClipID)
        {
            case 1:
                _audioSource.PlayOneShot(clip_1);
                break;
            default:
                break;
        }
    }
}
