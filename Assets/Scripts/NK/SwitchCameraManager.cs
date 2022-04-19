using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
 *  Utilizes the keyboard key 'P' for changing the camera.
 *
 * NK
 */

public class SwitchCameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineVirtualCamera tvCamera;
    
    
    private CameraSwitchAction _cameraSwitchAction;

    private void Awake()
    {
        _cameraSwitchAction = new CameraSwitchAction();
    }

    private void OnEnable()
    {
        _cameraSwitchAction.Enable();
        CameraSwitchHelper.Register(mainCamera);
        CameraSwitchHelper.Register(tvCamera);
        CameraSwitchHelper.SwitchCamera(mainCamera); // Must be here, otherwise the cameras will be bugged.
    }

    private void OnDisable()
    {
        _cameraSwitchAction.Disable();
        CameraSwitchHelper.Register(mainCamera);
        CameraSwitchHelper.Register(tvCamera);
    }

    private void Start()
    {
        _cameraSwitchAction.Camera.SwitchCamera.performed += _ => DetermineCamera();
        tvCamera.gameObject.SetActive(false);
    }

    private void DetermineCamera()
    {
        if (CameraSwitchHelper.LiveCamera(mainCamera))
        {
            CameraSwitchHelper.SwitchCamera(tvCamera);
        } 
        else if (CameraSwitchHelper.LiveCamera(tvCamera))
        {
            CameraSwitchHelper.SwitchCamera(mainCamera);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tvCamera.gameObject.SetActive(true);
        }
    }
}
