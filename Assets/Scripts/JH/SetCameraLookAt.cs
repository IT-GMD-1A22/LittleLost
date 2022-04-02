using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SetCameraLookAt : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;
    

    public void setCameraToPlayer()
    {
        var player = FindObjectOfType<CharacterController>().transform;
        vcam.Follow = player;
        vcam.LookAt = player;
    }

}
