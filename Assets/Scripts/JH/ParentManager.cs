using System;
using UnityEngine;

/*
 * Sets player to parent or unparent of object when triggerec
 *
 * JH
 */

public class ParentManager : MonoBehaviour
{
    
    private IObjectTransform _objectSetter;

    private void Awake()
    {
        _objectSetter = GetComponent<IObjectTransform>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _objectSetter.SetParent(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        _objectSetter.RemoveParent(other.transform);
    }
}