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

    [SerializeField] private bool hasGlowCircle;
    private Renderer glowCircleRenderer;

    [SerializeField] private bool hasFloorIndicator;
    private Renderer floorIndicatorRenderer;

    [SerializeField] private bool spawnActivated;

    private SpawnManager _spawnManager;
    
    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
        hasGlowCircle = transform.parent.Find("RespawnCircle").TryGetComponent(out Renderer glowCircleRendererOut);
        glowCircleRenderer = glowCircleRendererOut;
        hasFloorIndicator = transform.parent.Find("FloorIndicator").TryGetComponent(out Renderer floorIndicatorRendererOut);
        floorIndicatorRenderer = floorIndicatorRendererOut;
      
        if (hasGlowCircle)
            glowCircleRenderer.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        if (hasFloorIndicator)
            floorIndicatorRenderer.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawnActivated = true;
            _spawnManager.SetNewSpawnPoint(transform.parent);
        }
        if (hasGlowCircle)
            glowCircleRenderer.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        if (hasFloorIndicator)
            floorIndicatorRenderer.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }
}
