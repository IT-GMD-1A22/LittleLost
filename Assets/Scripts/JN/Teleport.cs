using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private string onColliderTag = "Untagged";
    [SerializeField] private Transform destination;

    [SerializeField] private bool playAudio = false;
    [SerializeField] private LevelAudioPlayer audioManager;
    [SerializeField] private string audioTag;

    private CharacterController _controller;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(onColliderTag)) {
            _controller = other.GetComponent<CharacterController>();
            _controller.enabled = false;
            other.transform.position = destination.position;
            _controller.enabled = true;
            if (playAudio && audioManager) 
                audioManager.PlayClipWithTag(audioTag);
            gameObject.SetActive(false);
        }
    }
}
