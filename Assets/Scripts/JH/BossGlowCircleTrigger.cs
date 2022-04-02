using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGlowCircleTrigger : MonoBehaviour
{
    private BossLevelManager _bossLevelManager;
    

    private void Awake()
    {
        _bossLevelManager = FindObjectOfType<BossLevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
