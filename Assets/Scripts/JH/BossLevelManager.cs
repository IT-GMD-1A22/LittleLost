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
    [SerializeField] private GameObject bossGameObject;
    [ReadOnly, SerializeField] private int limbsLeft;
    private readonly List<Transform> _bossLimbs = new List<Transform>();
    private readonly List<Transform> _disabledBossLimbs = new List<Transform>();

    [Header("Trigger Points")] 
    [SerializeField] private Transform[] firstLane = new Transform[2];
    [SerializeField] private Transform[] secondLane = new Transform[2];
    [SerializeField] private Transform[] thirdLane = new Transform[2];
    [SerializeField] private GameObject triggerIndicator;
    private readonly List<Transform[]> _lanePoints = new List<Transform[]>();
    
    [Header("Obstacles")] 
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Transform[] obstacleSpawnPoints;
    private GameObject _spawnedObstacle;

    [Header("Winner")] 
    [SerializeField] private Transform winnerPlayerPosition;

    [Header("Settings")] 
    [SerializeField] private float delayBetweenIndicators = 10f;
    
    private LevelAudioPlayer _levelAudioPlayer;
    private float _countdown = 10f;
    private GameObject _spawnedIndicator;
    private MarbleSpawner _marbleSpawner;

    private void Awake()
    {
        _marbleSpawner = FindObjectOfType<MarbleSpawner>();
        _levelAudioPlayer = FindObjectOfType<LevelAudioPlayer>();
    }

    private void Start()
    {
        if (bossGameObject)
        {
            foreach (Transform child in bossGameObject.transform)
            {
                _bossLimbs.Add(child);
                limbsLeft++;
            }
        }

        _lanePoints.Add(firstLane);
        _lanePoints.Add(secondLane);
        _lanePoints.Add(thirdLane);
    }


    private void Update()
    {
        if (_countdown <= 0f && !_spawnedIndicator)
        {
            _countdown = delayBetweenIndicators;
            SpawnAnIndicator();
        }


        _countdown -= Time.deltaTime;
    }

    private void SpawnAnIndicator()
    {
        var laneIndex = Random.Range(0, 3);
        var lane = _lanePoints[laneIndex];
        var randomPoint = GetRandomVectorBetweenTwoVectors(lane[0].position, lane[1].position);
        _spawnedIndicator = Instantiate(triggerIndicator, randomPoint, Quaternion.identity);
        _spawnedIndicator.transform.parent = transform;
    }

    private void SpawnAnObstacle()
    {
        Destroy(_spawnedObstacle);
        
        var point = obstacleSpawnPoints[Random.Range(0, obstacleSpawnPoints.Length)].position;
        
        _spawnedObstacle = Instantiate(obstaclePrefab, point + new Vector3(0f, 1.25f, 0f), Quaternion.identity);
        _spawnedObstacle.transform.parent = transform;
    }
    
    private void RemoveLimb()
    {
        if (_bossLimbs.Count > 1)
        {
            var index = Random.Range(1, _bossLimbs.Count); // dont take the head
            var limb = _bossLimbs[index].gameObject;
            _bossLimbs.RemoveAt(index);
            _disabledBossLimbs.Add(limb.transform);
            limb.SetActive(false);
            SpawnAnObstacle();
        }
        else if (_bossLimbs.Count == 1)
        {
            _marbleSpawner.ResetTimer();
            _marbleSpawner.enabled = false;
            Win();
        }


    }

    private void Win()
    {
        Debug.Log("Player Won the game.");
        var characterController = FindObjectOfType<CharacterController>();
        characterController.enabled = false;
        characterController.transform.position = winnerPlayerPosition.position;
        characterController.enabled = true;
        _levelAudioPlayer.PlayClipWithTag("winner");
    }

    private void ResetTimerAndIndicator()
    {
        _countdown = delayBetweenIndicators;
        Destroy(_spawnedIndicator);
        _spawnedIndicator = null; // ensure gbc
        _marbleSpawner.Tick();
        RemoveLimb();
    }

    private Vector3 GetRandomVectorBetweenTwoVectors(Vector3 pointA, Vector3 pointB)
    {
        var x = Random.Range(pointA.x, pointB.x);
        var y = Random.Range(pointA.y, pointB.y);
        var z = Random.Range(pointA.z, pointB.z);
        return new Vector3(x, y, z);
    }

    public void Restart()
    {
        _countdown = delayBetweenIndicators;
        Destroy(_spawnedIndicator);
        _spawnedIndicator = null; // ensure gbc
        _disabledBossLimbs.ForEach(x => x.gameObject.SetActive(true));
        _bossLimbs.AddRange(_disabledBossLimbs);
        _disabledBossLimbs.Clear();
    }
    public void IndicatorTriggered()
    {
        ResetTimerAndIndicator();
    }
}