using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorAudioTrigger : MonoBehaviour
{
    [SerializeField] private int ID_Collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NarratorSoundManager.Instance.PlaySoundClip(ID_Collider);
        }
    }
}
