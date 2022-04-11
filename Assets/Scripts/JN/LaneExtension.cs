using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneExtension : MonoBehaviour
{
    private PlayerLaneChanger _laneChanger;
    private PlayerInputActionAsset _playerInputAction;

    [SerializeField] private bool useLanes = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            _laneChanger = other.GetComponent<PlayerLaneChanger>();
            _laneChanger.enabled = useLanes;

            _playerInputAction = other.GetComponent<PlayerInputActionAsset>();
            _playerInputAction.takeZMovement = !useLanes;
        }
    }
}
