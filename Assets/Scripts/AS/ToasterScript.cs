using System;
using System.Collections;
using System.Collections.Generic;
using AS;
using UnityEngine;

public class ToasterScript : MonoBehaviour
{
    private LevelAudioPlayer _levelAudioPlayer;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Vector3 target;
    
    private void Awake()
    {
        _levelAudioPlayer = FindObjectOfType<LevelAudioPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (LittleKitchenLevelManager.BreadPickedUp.Equals(false))
        {
            _levelAudioPlayer.AddClipToQueue(audioClip);
        }
        else
        {
            other.transform.position = target;
        }
    }
}
