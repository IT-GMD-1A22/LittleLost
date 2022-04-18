using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerOnEnter : MonoBehaviour
{
    [Header("Custom Event Trigger")]
    public UnityEvent myEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (myEvent == null)
        {
            Debug.Log($"EventTriggerOnEnter was triggered, but {myEvent} was null");
        }
        else
        {
            Debug.Log($"EventTriggerOnEnter activated trigger: {myEvent}");
            myEvent.Invoke();
        }
    }
}
