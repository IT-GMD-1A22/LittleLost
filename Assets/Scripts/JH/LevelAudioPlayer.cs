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
    private void Awake()
    {
        _playQueue = new Queue<AudioClip>();
        _audioSource = GetComponent<AudioSource>();
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

    public void PlayClipWithTag(string tag)
    {
        var clips = _audioFiles.Find(x => x.type.Equals(tag));
        _playQueue.Enqueue(clips.clips[Random.Range(0, clips.clips.Length)]);
    }
    
    public void AddClipToQueue(AudioClip[] clip, bool interrupt = false)
    {
        if (interrupt)
        {
            _playQueue.Clear();
            _audioSource.Stop();
        }

        for (int i = 0; i < clip.Length; i++)
        {
            _playQueue.Enqueue(clip[i]);
        }
       
    }


    public float GetQueuePlayLength()
    {
        float length = 0f;
        foreach (var clip in _playQueue)
        {
            length += clip.length;
        }

        return length;
    }
    //
    // public IEnumerator PlayAudioClips(AudioClip[] clips)
    // {
    //     if (clips.Length > 0)
    //     {
    //         for (int i = 0; i < clips.Length; i++)
    //         {
    //             _audioSource.PlayOneShot(clips[i]);
    //             yield return new WaitForSeconds(clips[i].length);
    //         }
    //     }
    // }
    //
    // public void PlayAudioClipWithName(string name)
    // {
    //     var clip = audioFiles.SelectMany(af => af.clips)
    //         .FirstOrDefault(clip => clip.name.Equals(name));
    //     Debug.Log("playing clip: " + clip.name);
    //     if (clip != null)
    //     {
    //         _audioSource.PlayOneShot(clip);
    //     }
    // }
    // public void PlayAudioClip(string type, bool playAllInArray = false)
    // {
    //     try
    //     {
    //         var clips = audioFiles.Find(x => x.type.Equals(type));
    //         if (!playAllInArray)
    //         {
    //             _audioSource.PlayOneShot(clips.clips[Random.Range(0, clips.clips.Length)]);
    //         }
    //         else
    //         {
    //             StartCoroutine(PlayAudioClips(clips.clips));
    //         }
    //         
    //         
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.Log("No audio clip found: " + e.Message);
    //     }
    // }

    // public float GetClipsLength(string type)
    // {
    //     float length = 0f;
    //     var clips = audioFiles.Find(x => x.type.Equals(type));
    //     
    //     foreach (var clip in clips.clips)
    //     {
    //         length += clip.length;
    //     }
    //
    //     return length;
    // } 


}


[Serializable]
public struct AudioFiles
{
    public string type;
    public AudioClip[] clips;
}