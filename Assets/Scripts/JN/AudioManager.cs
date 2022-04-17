using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Stores audio clips to be played on command.
 *
 * JN based on JH audio level player
 */
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Serializable]
    private class Audio
    {
        public string playOnEventName;
        public bool playOnce = false;
        public AudioClip[] clips;
        private int _playCount = 0;

        public void Play()
        {
            if (playOnce) 
            {
                if (_playCount == 0)
                    _playQueue.Enqueue(clips[Random.Range(0, clips.Length)]);
            }
            else 
            {
                _playQueue.Enqueue(clips[Random.Range(0, clips.Length)]);
            }
            _playCount ++;
        }
    }

    [SerializeField] private List<Audio> _audioFiles;
    
    private AudioSource _audioSource;
    private static Queue<AudioClip> _playQueue;
    private bool playing;
    private void Awake()
    {
        _playQueue = new Queue<AudioClip>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnEnable ()
    {
        if (_audioFiles.Any())
            _audioFiles.ForEach(a => EventManager.StartListening (a.playOnEventName, a.Play));
    }

    void OnDisable ()
    {
        if (_audioFiles.Any())
            _audioFiles.ForEach(a => EventManager.StopListening (a.playOnEventName, a.Play));
    }


    private void Update()
    {
        if (_playQueue.Count > 0 && playing == false)
        {
            playing = true;
            StartCoroutine(PlayClip(_playQueue.Dequeue()));
        }
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
            _playQueue.Clear();
            _audioSource.Stop();
        }
        _playQueue.Enqueue(clip);
    }

    // public void PlayClipWithTag(string tag)
    // {
    //     var clips = _audioFiles.Find(x => x.playOnEventName.Equals(tag));
    //     _playQueue.Enqueue(clips.clips[Random.Range(0, clips.clips.Length)]);
    // }

    // public void PlayClipWithTagInterruptFirst(string tag)
    // {
    //     _playQueue.Clear();
    //     _audioSource.Stop();
    //     PlayClipWithTag(tag);
    // }
    
    // public void AddClipToQueue(AudioClip[] clip, bool interrupt = false)
    // {
    //     if (interrupt)
    //     {
    //         _playQueue.Clear();
    //         _audioSource.Stop();
    //     }

    //     for (int i = 0; i < clip.Length; i++)
    //     {
    //         _playQueue.Enqueue(clip[i]);
    //     }
       
    // }
    
    // public float GetQueuePlayLength()
    // {
    //     float length = 0f;
    //     foreach (var clip in _playQueue)
    //     {
    //         length += clip.length;
    //     }

    //     return length;
    // }
}
