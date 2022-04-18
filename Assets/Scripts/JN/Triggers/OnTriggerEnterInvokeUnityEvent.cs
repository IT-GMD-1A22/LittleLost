using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterInvokeUnityEvent : MonoBehaviour
{
    [SerializeField] UnityEvent unityEvent;

    void OnTriggerEnter ()
    {
        InvokeEvent();
    }

    private void InvokeEvent()
    {
        if (unityEvent != null)
           unityEvent.Invoke();
    }
}
