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
    private class AudioCollection
    {
        public string addRandomToQueueOnEvent;
        public AudioClip[] clips;
        public void AddToQueue()
        {
            _playQueue.Enqueue(clips[Random.Range(0, clips.Length)]);
        }
    }
    
    [Serializable]
    private class AudioFile
    {
        public string playOnEventName;
        public bool playOnce = false;
        public AudioClip file;
        private int _playCount = 0;

        public void Play()
        {
            if (playOnce) 
            {
                if (_playCount == 0)
                _audioSource.PlayOneShot(file);
            }
            else 
            {
                _audioSource.PlayOneShot(file);
            }
            _playCount ++;
        }
    }

    [SerializeField] private List<AudioFile> instantAudio;
    [SerializeField] private List<AudioCollection> queuedAudio;
    
    private static AudioSource _audioSource;
    private static Queue<AudioClip> _playQueue;
    private bool playing;
    private void Awake()
    {
        _playQueue = new Queue<AudioClip>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnEnable ()
    {
        if (queuedAudio.Any())
            queuedAudio.ForEach(a => EventManager.StartListening (a.addRandomToQueueOnEvent, a.AddToQueue));
            instantAudio.ForEach(a => EventManager.StartListening (a.playOnEventName, a.Play));
    }

    void OnDisable ()
    {
        if (queuedAudio.Any())
            queuedAudio.ForEach(a => EventManager.StopListening (a.addRandomToQueueOnEvent, a.AddToQueue));
            instantAudio.ForEach(a => EventManager.StopListening (a.playOnEventName, a.Play));
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
}
