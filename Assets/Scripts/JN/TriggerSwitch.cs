using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Needs a collider isTrigger and a ridgidBody !isGravity and isKinematic.

public class TriggerSwitch : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSwitch;
    [SerializeField] private string onColliderTag = "Untagged";
    [SerializeField] private bool enableOnEnter = true;

    // Enables objects when a specified collider tag enters the trigger. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(onColliderTag) && objectsToSwitch.Any())
            objectsToSwitch.ForEach(o => o.SetActive(enableOnEnter));
    }

    // Disables objects when a specified collider tag exits the trigger. 
    private void OnTriggerExit(Collider other)
    {
         if (other.CompareTag(onColliderTag) && objectsToSwitch.Any())
            objectsToSwitch.ForEach(o => o.SetActive(!enableOnEnter));
    }
}
