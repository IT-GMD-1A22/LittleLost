using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayAudio : MonoBehaviour
{
    [SerializeField] private LevelAudioPlayer audioManager;
    [SerializeField] private string audioTag;
    [SerializeField] private float delayBeforePlaying = 0.0f;
    [SerializeField] private bool playOnce = false;
    private int playCount = 0;

    private void Play() {
        if (audioManager) {
            audioManager.PlayClipWithTag(audioTag);
            playCount++;
        }
    }

    private IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delayBeforePlaying);
        if (playOnce) {
            if (playCount == 0)
                Play();
        }
        else {
            Play();
        }
    }

   private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(RunEvent());
    }

}
