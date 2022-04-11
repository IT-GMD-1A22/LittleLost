using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/*
 *  Script to handle spawning and spawnpoints.
 *
 *
 * JH
 */
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private bool Spawn;
    [HideInInspector] public GameObject currentPlayer { get; private set; }

    private CinemachineVirtualCamera cvm;
    private void Awake()
    {
        cvm = FindObjectOfType<CinemachineVirtualCamera>();
    }

    public void SpawnPlayer()
    {
        if (currentPlayer != null)
        {
            Destroy(currentPlayer);
        }
        
        var player = Instantiate(Player, spawnPoint.position, Player.transform.rotation);
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
