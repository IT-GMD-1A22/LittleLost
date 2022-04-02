using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Handles boss level
 *
 * JH
 */
public class BossLevelManager : MonoBehaviour
{   
    [Header("Boss")]
    [SerializeField] private GameObject Boss;
    private List<Transform> BossLimbs = new List<Transform>();
    [ReadOnly, SerializeField] private int LimbsLeft;

    [Header("Trigger Points")]
    [SerializeField] private Transform[] firstLane = new Transform[2];
    [SerializeField] private Transform[] secondLane = new Transform[2];
    [SerializeField] private Transform[] thirdLane = new Transform[2];
    [SerializeField] private GameObject TriggerIndicator;
    private List<Transform[]> lanePoints = new List<Transform[]>();

    [Header("MarbleSpawner")] 
    [SerializeField] private MarbleSpawner _marbleSpawner;
    
    [Header("Settings")] 
    [SerializeField] private float timeBeforeSpawningNew = 10f;


    private float countdown = 10f;
    private GameObject spawnedIndicator;

    private void Awake()
    {
        _marbleSpawner = FindObjectOfType<MarbleSpawner>();
    }

    private void Start()
    {
        if (Boss)
        {
            foreach (Transform child in Boss.transform)
            {
                BossLimbs.Add(child);
                LimbsLeft++;
            }
        }
        
        lanePoints.Add(firstLane);
        lanePoints.Add(secondLane);
        lanePoints.Add(thirdLane);
    }


    private void Update()
    {
        if (countdown <= 0f && !spawnedIndicator)
        {
            countdown = timeBeforeSpawningNew;
            spawnIndicator();
            
        }


        countdown -= Time.deltaTime;
    }

    private void spawnIndicator()
    {
        var laneIndex = Random.Range(0, 2);
        var lane = lanePoints[laneIndex];
        var randomPoint = GetRandomVectorBetweenTwoVectors(lane[0].position, lane[1].position);
        spawnedIndicator = Instantiate(TriggerIndicator, randomPoint, Quaternion.identity);
        spawnedIndicator.transform.parent = transform;
        
    }
    
    public void resetTimerAndIndicator()
    {
        countdown = timeBeforeSpawningNew;
        Destroy(spawnedIndicator);
        spawnedIndicator = null; // ensure gbc
    }

    private Vector3 GetRandomVectorBetweenTwoVectors(Vector3 pointA, Vector3 pointB)
    {
        var x = Random.Range(pointA.x, pointB.x);
        var y = Random.Range(pointA.y, pointB.y);
        var z = Random.Range(pointA.z, pointB.z);
        return new Vector3(x, y, z);
    }
    
}
