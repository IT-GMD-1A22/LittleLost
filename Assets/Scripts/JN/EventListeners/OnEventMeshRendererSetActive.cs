using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventMeshRendererSetActive : MonoBehaviour
{
    [SerializeField] private string activeOnEventName = "";
    [SerializeField] private string inactiveOnEventName = "";
    [SerializeField] private bool renderOnAwake = false;
    private MeshRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        if (renderOnAwake)
            SetActive();
        else 
            SetInactive();
    }

    void OnEnable ()
    {
        EventManager.StartListening (activeOnEventName, SetActive);
        EventManager.StartListening (inactiveOnEventName, SetInactive);
    }

    void OnDisable ()
    {
        EventManager.StopListening (activeOnEventName, SetActive);
        EventManager.StopListening (inactiveOnEventName, SetInactive);
    }

    private void SetActive() {
        if (_renderer) 
            _renderer.enabled = true;
    }

    private void SetInactive() {
        if (_renderer)
            _renderer.enabled = false;
    }
}
