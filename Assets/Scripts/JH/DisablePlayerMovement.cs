using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerMovement : MonoBehaviour
{
    private SpawnManager _spawnManager;


    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }


    public void DisableMovement()
    {
        var controller = _spawnManager.currentPlayer.GetComponent<PlayerController>();
        controller.disablePlayerInput = true;
    }


    public void EnableMovement()
    {
        var controller = _spawnManager.currentPlayer.GetComponent<PlayerController>();
        controller.disablePlayerInput = false;
    }

    
}
