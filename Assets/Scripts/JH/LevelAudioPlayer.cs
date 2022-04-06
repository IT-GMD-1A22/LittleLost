using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Stores audio clips to be played on command.
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

    private void Awake()
    {
        _playQueue = new Queue<AudioClip>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (_playQueue.Count <= 0 || playing) return;
        
        Debug.Log(Time.time);

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
        _playQueue.Enqueue(clips.clips[Random.Range(0, clips.clips.Length)]);
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