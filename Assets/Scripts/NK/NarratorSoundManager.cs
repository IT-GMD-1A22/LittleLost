using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * Script that handles and plays the corresponding id of an narrator TTS.
 * Setup as an Singleton, ensuring the class only exists once and can be utilized in other classes without calling GetComponent<>.
 * 
 * - NK
 */

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
    }
}
