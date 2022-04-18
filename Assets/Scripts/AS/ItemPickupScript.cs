using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Pickupable"))
            return;
        LittleKitchenLevelManager.BreadPickedUp = true;
        Destroy(gameObject);
    }
}
