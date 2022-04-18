using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterSetRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private string onColliderTag = "Untagged";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(onColliderTag)) 
            other.transform.rotation = Quaternion.Euler(rotation);
    }
}
