using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventLightOnOff : MonoBehaviour
{
    [SerializeField] private string onEventName = "";
    [SerializeField] private string offEventName = "";
    [SerializeField] private bool lightOnAwake = false;
    private Light _light;

    void Awake()
    {
        _light = GetComponent<Light>();
        if (lightOnAwake) 
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
        if (_light) 
            _light.enabled = true;
    }

    private void SetOff() {
        if (_light)
            _light.enabled = false;
    }
}
