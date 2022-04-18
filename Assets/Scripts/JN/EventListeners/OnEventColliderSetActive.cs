using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventColliderSetActive : MonoBehaviour
{
    [SerializeField] private string activeOnEventName = "";
    [SerializeField] private string inactiveOnEventName = "";
    [SerializeField] private bool activeOnce = false;
    [SerializeField] private bool colliderActiveOnAwake = false;
    private Collider _collider;

    private int _activeCount = 0;

    void Awake()
    {
        _collider = GetComponent<Collider>();
        if (colliderActiveOnAwake)
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

    private bool CanCollide() {
        if (activeOnce) {
            return _activeCount == 0 ? true : false;
        }
        else {
            return true;
        }
    }

    private void SetActive() {
        if (_collider && CanCollide()) {
            _collider.enabled = true;
            _activeCount ++;
        }
    }

    private void SetInactive() {
        if (_collider)
            _collider.enabled = false;
    }
}
