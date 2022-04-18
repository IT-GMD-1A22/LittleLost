using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * AudioManager that stores audio and exposes
 * methods to play audio tags.
 *
 * JH
 */
[RequireComponent(typeof(AudioSource))]
public class LevelAudioPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioFiles> _audioFiles;

    private AudioSource _audioSource;
    private Queue<AudioClip> _playQueue;
    private bool playing;
    private Coroutine currentlyPlaying;
    private int lastRandom;

    private void Awake()
    {
        _playQueue = new Queue<AudioClip>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (_playQueue.Count <= 0 || playing) return;

        playing = true;
        currentlyPlaying = StartCoroutine(PlayClip(_playQueue.Dequeue()));
    }

    private IEnumerator PlayClip(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        playing = false;
    }

    public void AddClipToQueue(AudioClip clip, bool interrupt = false)
    {
        if (interrupt)
        {
            StopCurrentlyPlayingAndClearQueue();
        }

        _playQueue.Enqueue(clip);
    }



    public void PlayClipWithTag(string tag)
    {
        var clips = _audioFiles.Find(x => x.type.Equals(tag));
        if (clips.clips == null) return;
            var rand = Random.Range(0, clips.clips.Length);
        if (clips.clips.Length > 1 && rand == lastRandom)
        {
            while (rand == lastRandom)
            {
                rand = Random.Range(0, clips.clips.Length);
            }
        }

        _playQueue.Enqueue(clips.clips[rand]);
    }

    public void PlayClipWithTagInterruptFirst(string tag)
    {
        StopCurrentlyPlayingAndClearQueue();
        PlayClipWithTag(tag);
    }

    public void AddClipToQueue(AudioClip[] clip, bool interrupt = false)
    {
        if (interrupt)
        {
            StopCurrentlyPlayingAndClearQueue();
        }

        foreach (var t in clip)
        {
            _playQueue.Enqueue(t);
        }
    }
    
    public void StopCurrentlyPlayingAndClearQueue()
    {
        if (currentlyPlaying != null)
        {
            StopCoroutine(currentlyPlaying);
            currentlyPlaying = null;
        }

        playing = false;
        _playQueue.Clear();
        _audioSource.Stop();
    }
    
    public float GetQueuePlayLength()
    {
        return _playQueue.Sum(clip => clip.length);
    }
}


[Serializable]
public struct AudioFiles
{
    public string type;
    public AudioClip[] clips;
}