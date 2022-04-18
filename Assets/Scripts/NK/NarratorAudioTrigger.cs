using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Security;
using UnityEngine;
using UnityEngine.Serialization;

public class NarratorAudioTrigger : MonoBehaviour
{
    [SerializeField] private int colliderId = 0;
    [SerializeField] private bool playOnce = true;
    [SerializeField] private bool disablePlayer = true;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playOnce)
            {
                print("Play once");
                // _playerController.enabled = false;
                NarratorSoundManager.Instance.PlaySoundClip(colliderId);
                // _playerController.enabled = true;
            }
            else
            {
                NarratorSoundManager.Instance.PlaySoundClip(colliderId);
                print("Play mult times");
            }
        }
    }
}
