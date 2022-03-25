using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Spawn beacon indicator, turns on and off emission to indicate
 * whether the beacon has been activated or not.
 *
 * JH
 */
public class SpawnBeaconManager : MonoBehaviour
{
    private const string EMISSION_PROPERTY = "_EMISSION";

    [SerializeField] private Renderer[] enableGlowOnTrigger;
    [SerializeField] private Renderer[] disableGlowOnTrigger;
    
    [ReadOnly, SerializeField] private bool spawnActivated;
    private SpawnManager _spawnManager;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();

        foreach (var glowElement in enableGlowOnTrigger)
        {
            glowElement.material.DisableKeyword(EMISSION_PROPERTY);
        }

        foreach (var glowElement in disableGlowOnTrigger)
        {
            glowElement.material.EnableKeyword(EMISSION_PROPERTY);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && spawnActivated == false)
        {
            spawnActivated = true;
            _spawnManager.SetNewSpawnPoint(transform.parent);


            foreach (var glowElement in enableGlowOnTrigger)
            {
                glowElement.material.EnableKeyword(EMISSION_PROPERTY);
            }

            foreach (var glowElement in disableGlowOnTrigger)
            {
                glowElement.material.DisableKeyword(EMISSION_PROPERTY);
            }
        }
    }
}