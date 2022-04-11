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
        var controller = _spawnManager.currentPlayer.GetComponent<PlayerController>();
        controller.disablePlayerInput = true;

    }


    public void EnableMovement()
    {
        var controller = _spawnManager.currentPlayer.GetComponent<PlayerController>();
        controller.disablePlayerInput = false;
    }

    
}
