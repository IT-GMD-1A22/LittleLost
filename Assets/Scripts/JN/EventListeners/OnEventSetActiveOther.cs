using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventSetActiveOther : MonoBehaviour
{
    [SerializeField] private string activeOnEventName = "";
    [SerializeField] private string inactiveOnEventName = "";

    [SerializeField] private GameObject setActive;

    [SerializeField] private bool activeOnce = false;

    private int _activeCount = 0;

    void OnEnable ()
    {
        EventManager.StartListening (activeOnEventName, Active);
        EventManager.StartListening (inactiveOnEventName, Inactive);
    }

    void OnDisable ()
    {
        EventManager.StopListening (activeOnEventName, Active);
        EventManager.StopListening (inactiveOnEventName, Inactive);
    }

    private bool CanActivate() {
        if (activeOnce) {
            return _activeCount == 0 ? true : false;
        }
        else {
            return true;
        }
    }

    private void Active() {
        if (setActive && CanActivate()) {
            setActive.SetActive(true);
            _activeCount ++;
        }
    }

    private void Inactive() {
        if (setActive)
            setActive.SetActive(false);
    }
}
