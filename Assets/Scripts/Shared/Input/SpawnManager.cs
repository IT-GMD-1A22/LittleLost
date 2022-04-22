using System.Collections;
using Cinemachine;
using UnityEngine;

/*
 *  Script to handle spawning and spawnpoints.
 *  - Singleton
 *
 * JH
 */
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform debugSpawnPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private bool Spawn;
    [SerializeField] private bool debugSpawn;
    [HideInInspector] public GameObject currentPlayer { get; private set; }

    private CinemachineVirtualCamera cvm;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        cvm = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void SpawnPlayer()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }

        Vector3 point = debugSpawn ? debugSpawnPoint.position : spawnPoint.position;
        
        var player = Instantiate(Player, point, Player.transform.rotation);
        // let camera follow the new player
        cvm.m_Follow = player.transform;
        cvm.m_LookAt = player.transform;
        currentPlayer = player;
        StartCoroutine(DisableMovementUntil(1f));
    }

    private IEnumerator DisableMovementUntil(float delay)
    {
        var controller = currentPlayer.GetComponent<PlayerController>();
        controller.disablePlayerInput = true;
        yield return new WaitForSeconds(delay);
        controller.disablePlayerInput = false;
    }
    
    public void SetNewSpawnPoint(Transform position)
    {
        spawnPoint = position;
    }

    private void Update()
    {
        if (Spawn)
        {
            Spawn = false;
            SpawnPlayer();
        }
    }

    public Transform GetCurrentSpawnPoint()
    {
        return spawnPoint;
    }
}
