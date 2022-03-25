using System;
using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float waitForSeconds = 1f;
    [SerializeField] private float strength = 5f;
    [SerializeField] private Vector3 pushDirection;
    

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(KnockBackCharacter(other));
        }
    }


    private IEnumerator KnockBackCharacter(Collider other)
    {
        var controller = other.GetComponent<PlayerController>();
        controller.disablePlayerInput = true;
        controller.AddExternalForce(pushDirection, strength);
        yield return new WaitForSeconds(waitForSeconds);
        controller.disablePlayerInput = false;
    }
    
    
    
}