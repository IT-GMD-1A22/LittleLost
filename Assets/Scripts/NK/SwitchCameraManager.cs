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
    private AudioSource _audioSource;

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
        _audioSource = GetComponent<AudioSource>();
        tvCamera.gameObject.SetActive(false);
    }

    private void DetermineCamera()
    {
        if (CameraSwitchHelper.LiveCamera(mainCamera))
        {
            StartCoroutine(SoundVolumeFade(0.6f, 2f));
            CameraSwitchHelper.SwitchCamera(tvCamera);
        } 
        else if (CameraSwitchHelper.LiveCamera(tvCamera))
        {
            StartCoroutine(SoundVolumeFade(0f, 2f));
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
    
    private IEnumerator SoundVolumeFade(float endingValue, float fadeDuration)
    {
        float time = 0;
        float startingVolume = _audioSource.volume;
        while (time < fadeDuration)
        {
            _audioSource.volume = Mathf.Lerp(startingVolume, endingValue, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        _audioSource.volume = endingValue;
    }
}
