using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterPlayerInput : MonoBehaviour
{
    private PlayerController _controller;

    [SerializeField] private bool disableInput = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            _controller = other.GetComponent<PlayerController>();
            _controller.disablePlayerInput = disableInput;
        }
    }
}
