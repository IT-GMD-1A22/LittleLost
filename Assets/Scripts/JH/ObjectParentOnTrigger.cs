using UnityEngine;

/*
 * If player trigger stay or exit, parent or unparent player to trigger
 *
 * JH
 */
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
