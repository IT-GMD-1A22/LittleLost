using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnTriggerEnable : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToEnable;
    [SerializeField] private string onColliderTag = "Untagged";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(onColliderTag) && objectsToEnable.Any())
            objectsToEnable.ForEach(o => o.SetActive(true));
    }
}
