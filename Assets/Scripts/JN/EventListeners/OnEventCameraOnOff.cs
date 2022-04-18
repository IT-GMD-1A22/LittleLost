using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventCameraOnOff : MonoBehaviour
{
    [SerializeField] private string onEventName = "";
    [SerializeField] private string offEventName = "";
    [SerializeField] private bool FilmOnAwake = false;
    private Camera _cam;

    void Awake()
    {
        _cam = GetComponent<Camera>();
        if (FilmOnAwake)
            SetOn();
        else 
            SetOff();
    }

    void OnEnable ()
    {
        EventManager.StartListening (onEventName, SetOn);
        EventManager.StartListening (offEventName, SetOff);
    }

    void OnDisable ()
    {
        EventManager.StopListening (onEventName, SetOn);
        EventManager.StopListening (offEventName, SetOff);
    }

    private void SetOn() {
        if (_cam) 
            _cam.enabled = true;
    }

    private void SetOff() {
        if (_cam)
            _cam.enabled = false;
    }
}
