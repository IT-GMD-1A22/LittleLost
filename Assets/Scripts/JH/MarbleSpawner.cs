using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MarbleSpawner : MonoBehaviour
{
    [SerializeField] private bool isEnabled;
    [SerializeField] private Transform LeftSpawnPoint;
    [SerializeField] private Transform RightSpawnPoint;
    [SerializeField] private List<GameObject> MarblePrefabs;
    [SerializeField] private float maxSpawnTimer = 1f;
    [SerializeField] private float minSpawnTimer = 0.05f;
    [SerializeField] private float tick = 0.12f;
    [SerializeField] private float destroyTimer = 7f;

    private float initialMaxSpawnTimer;
    private float spawnTimer;

    private void Awake()
    {
        initialMaxSpawnTimer = maxSpawnTimer;
    }
    private void Update()
    {
        if (spawnTimer <= 0f && isEnabled)
        {
            spawnTimer = maxSpawnTimer;
            var marble = Instantiate(MarblePrefabs[Random.Range(0, MarblePrefabs.Count)], GetRandomPoint(), Quaternion.identity);
            Destroy(marble, destroyTimer);

        }
        spawnTimer -= Time.deltaTime;
    }
    
    private Vector3 GetRandomPoint ()
    {
        float x = Random.Range(RightSpawnPoint.position.x, LeftSpawnPoint.position.x);
        float y = Random.Range(LeftSpawnPoint.position.y - 1f, LeftSpawnPoint.position.y + 1);
        float z = Random.Range(LeftSpawnPoint.position.z - 0.2f, LeftSpawnPoint.position.z + 0.2f);
        
        return new Vector3(x, y, z);
    }

    public void StartSpawning()
    {
        isEnabled = true;
    }
    
    public void ResetTimer()
    {
        StartCoroutine(DestroyAllMarbles());
    }

    public void Tick()
    {
        if (maxSpawnTimer > minSpawnTimer)
        {
            maxSpawnTimer -= tick;
        }
    }

    private IEnumerator DestroyAllMarbles()
    {
        maxSpawnTimer = initialMaxSpawnTimer;
        spawnTimer = 4f;
        
        var marbles = GameObject.FindGameObjectsWithTag("Marble");
        foreach (var marble in marbles)
        {
            // remove the rigidbody to freeze the marbles.
            Destroy(marble.GetComponent<Rigidbody>());
        }
        
        yield return new WaitForSeconds(3f);
        
        foreach (var marble in marbles)
        {
            Destroy(marble);
        }
    }
}
