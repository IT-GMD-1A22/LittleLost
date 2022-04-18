using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenEventTrigger : MonoBehaviour
{
    private LittleKitchenLevelManager _manager;

    private void Awake()
    {
        _manager = FindObjectOfType<LittleKitchenLevelManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        _manager.BeginCooktopTrap();
    }
}
