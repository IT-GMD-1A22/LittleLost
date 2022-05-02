using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Security;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Script that triggers the narrator TTS clip when enterted.
 * - NK
 */
public class NarratorAudioTrigger : MonoBehaviour
{
    [SerializeField] private int colliderId = 0;
    [SerializeField] private bool playOnce = true;
    [SerializeField] private bool playMultiple = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playOnce)
            {
                NarratorSoundManager.Instance.PlaySoundClip(colliderId);
                playOnce = false;
            }

            if (playMultiple)
            {
                NarratorSoundManager.Instance.PlaySoundClip(colliderId);
            }
        }
    }
}
