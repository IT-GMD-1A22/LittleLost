using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string onColliderTag = "Untagged";
    [SerializeField] private Vector3 destination;

    [SerializeField] private bool playAudio = false;
    [SerializeField] private LevelAudioPlayer audioManager;
    [SerializeField] private string audioTag;
    
    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(onColliderTag)) {
            if (playAudio) 
                Play();
            other.transform.position = destination;
        }
    }

    private void Play() {
        if (audioManager) {
            audioManager.PlayClipWithTag(audioTag);
        }
    }
}
