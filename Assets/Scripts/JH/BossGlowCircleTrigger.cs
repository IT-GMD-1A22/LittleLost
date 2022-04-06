using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGlowCircleTrigger : MonoBehaviour
{
    private BossLevelManager _bossLevelManager;
    private LevelAudioPlayer _levelAudioPlayer;
    [SerializeField] private AudioClip triggerSound;

    private void Awake()
    {
        _bossLevelManager = FindObjectOfType<BossLevelManager>();
        _levelAudioPlayer = FindObjectOfType<LevelAudioPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _bossLevelManager.IndicatorTriggered();
            _levelAudioPlayer.AddClipToQueue(triggerSound);
        }
    }
}
