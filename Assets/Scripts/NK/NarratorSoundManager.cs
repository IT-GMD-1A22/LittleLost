using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class NarratorSoundManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioClips;
    public static NarratorSoundManager Instance;
    private AudioSource _audioSource;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundClip(int clip)
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(audioClips.ElementAt(clip));

        // switch (clip)
        // {
        //     case 1:
        //         _audioSource.PlayOneShot(audioClips.ElementAt(clip));
        //         break;
        //     case 2:
        //         _audioSource.PlayOneShot(audioClips.ElementAt(clip));
        //         break;
        //     case 3:
        //         _audioSource.PlayOneShot(audioClips.ElementAt(clip));
        //         break;
        //     default:
        //         break;
        // }
    }
}
