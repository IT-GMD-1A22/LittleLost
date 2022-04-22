using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Plays audio when invoked by LevelTriggerManager
 */
[RequireComponent(typeof(LevelTriggerManager))]
public class LevelTriggerPlayAudio : MonoBehaviour, ITrigger
{
    [SerializeField] private float delay;
    [SerializeField] private List<AudioClip> _audioClips;
    [SerializeField] private bool interruptAudio;
    [SerializeField] private bool completeTriggerAfterAudio;
    private LevelAudioPlayer _levelAudioPlayer;
    public bool isCompleted { get; set; }

    private void Awake()
    {
        _levelAudioPlayer = FindObjectOfType<LevelAudioPlayer>();
    }

    private IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        if (interruptAudio)
        {
            _levelAudioPlayer.StopCurrentlyPlayingAndClearQueue();
        }
        
        _levelAudioPlayer.AddClipToQueue(_audioClips.ToArray());
        
        if (completeTriggerAfterAudio)
        {
            yield return new WaitForSeconds(_levelAudioPlayer.GetQueuePlayLength());
        }

        isCompleted = true;
    }

    public void Invoke()
    {
        StartCoroutine(RunEvent());
    }
}