using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnTriggerDisable : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDisable;
    [SerializeField] private string onColliderTag = "Untagged";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(onColliderTag) && objectsToDisable.Any())
            objectsToDisable.ForEach(o => o.SetActive(false));
    }
}
