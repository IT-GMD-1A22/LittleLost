using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * Event trigger to handle animations (Could also have been used for other things, but is only used for animation)
 * - NK
 */
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
