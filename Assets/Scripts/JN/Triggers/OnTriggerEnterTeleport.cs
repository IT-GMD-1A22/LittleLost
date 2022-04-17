using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterTeleport : MonoBehaviour
{
    [SerializeField] private string onColliderTag = "Untagged";
    [SerializeField] private Transform destination;

    private CharacterController _controller;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(onColliderTag)) {
            _controller = other.GetComponent<CharacterController>();
            _controller.enabled = false;
            other.transform.position = destination.position;
            _controller.enabled = true;
        }
    }
}
