using System;
using UnityEngine;

public class LittleKitchenLevelManager : MonoBehaviour
{
    public static bool BreadPickedUp = false;
    
    public void BeginKnifeTrap()
    {
        
    }

    public void BeginCooktopTrap()
    {
        GameObject[] cooktops;

        cooktops = GameObject.FindGameObjectsWithTag("Cooktop");
        
        foreach (var cooktop in cooktops)
        {
            var particleSystem = cooktop.GetComponent(typeof(ParticleSystem)) as ParticleSystem;

            if (particleSystem!.isPlaying)
            {
                particleSystem.Stop();
            }

            var colliderObject = transform.GetChild(0);
            var collider = colliderObject.GetComponent<Collider>();
            collider.enabled = false;

        }
    }

}
