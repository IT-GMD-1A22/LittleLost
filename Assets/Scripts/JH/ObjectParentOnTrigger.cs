using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParentOnTrigger : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
            other.transform.parent = transform.parent;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.transform.parent = null;
    }
}