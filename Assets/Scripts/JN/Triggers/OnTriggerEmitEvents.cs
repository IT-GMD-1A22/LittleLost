using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnTriggerEmitEvents : MonoBehaviour
{
    [Header("Events")] 
    [SerializeField] private List<string> onEnterEvents;
    [SerializeField] private List<string> onExitEvents;


    [Header("Collider")] 
    [SerializeField] private bool useColliderTags = false;
    [SerializeField] private List<string> colliderTags;


    private void InvokeEnterEvents() {
        if (onEnterEvents.Any())
            onEnterEvents.ForEach(e => EventManager.TriggerEvent(e));
    }

    private void InvokeExitEvents() {
        if (onExitEvents.Any())
            onExitEvents.ForEach(e => EventManager.TriggerEvent(e));
    }

    private bool TagsMatch(string tag) {
        if (colliderTags.Any() && colliderTags.Contains(tag)) {
            return true;
        }
        else {
            return false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (useColliderTags) {
            if (TagsMatch(other.tag)) {
                InvokeEnterEvents();
            }
        }
        else {
            InvokeEnterEvents();
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if (useColliderTags) {
            if (TagsMatch(other.tag)) {
                InvokeExitEvents();
            }
        }
        else {
            InvokeExitEvents();
        }
    }
}
