using System;
using System.Collections;
using System.Collections.Generic;
using AS;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour
{
    private LevelAudioPlayer _levelAudioPlayer;
    [SerializeField] private AudioClip audioClip;
    
    private void Awake()
    {
        _levelAudioPlayer = FindObjectOfType<LevelAudioPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Pickupable"))
            return;
        LittleKitchenLevelManager.BreadPickedUp = true;
        LittleKitchenLevelManager.LevelStage = 3;
        _levelAudioPlayer.AddClipToQueue(audioClip);
        Destroy(gameObject);
    }
}
