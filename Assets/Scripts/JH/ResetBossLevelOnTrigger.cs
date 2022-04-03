using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBossLevelOnTrigger : MonoBehaviour
{

    private MarbleSpawner _marbleSpawner;
    private BossLevelManager _bossLevelManager;

    private void Awake()
    {
        _marbleSpawner = FindObjectOfType<MarbleSpawner>();
        _bossLevelManager = FindObjectOfType<BossLevelManager>();
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ResetLevel();
        }
    }
    
    private void ResetLevel()
    {
        _marbleSpawner.ResetTimer();
        _bossLevelManager.Restart();
        
    }
}
