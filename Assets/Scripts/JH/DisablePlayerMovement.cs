using UnityEngine;

/*
 * Script that finds current player and
 * disables / enables movement.
 *
 * Can be triggered by LevelTriggerManager.
 *
 * JH
 */
public class DisablePlayerMovement : MonoBehaviour
{
    private SpawnManager _spawnManager;

    private void Awake()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }
    
    public void DisableMovement()
    {
        GetPlayerController().disablePlayerInput = true;

    }

    public void EnableMovement()
    {
        GetPlayerController().disablePlayerInput = false;
    }

    private PlayerController GetPlayerController()
    {
        return _spawnManager.currentPlayer.GetComponent<PlayerController>();
    }
}
