using Cinemachine;
using UnityEngine;

/*
 * Set virtual camera lookat and follow to player
 *
 * JH
 */
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
