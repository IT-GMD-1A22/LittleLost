using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterSetColliderAsChild : MonoBehaviour
{
    [SerializeField] GameObject parent;
    private void OnTriggerEnter(Collider other)
    {
        if (parent && other.tag == "Player")
            other.transform.parent = parent.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (parent && other.tag == "Player")
            other.transform.parent = null;
    }
}
