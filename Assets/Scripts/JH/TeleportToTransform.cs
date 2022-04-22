using System.Collections.Generic;
using UnityEngine;


/*
 * Teleport player to transform when invoked.
 * 
 * -JH
 */
public class TeleportToTransform : MonoBehaviour, ITrigger
{
    [SerializeField] private List<Transform> orderedTransforms = new();
    [SerializeField] private bool removeFromListAfterSpawn = true;
    public bool isCompleted { get; set; }

    public void Invoke()
    {
        if (orderedTransforms.Count <= 0) return;
        TeleportPlayer();
        RemoveBeacons();
        isCompleted = true;
    }

    private void TeleportPlayer()
    {
        SpawnManager.Instance
            .currentPlayer
            .GetComponent<PlayerController>()
            .TeleportPlayerToVector(orderedTransforms[0].position);

        
    }

    private void RemoveBeacons()
    {
        if (removeFromListAfterSpawn)
            orderedTransforms.RemoveAt(0);
    }
}