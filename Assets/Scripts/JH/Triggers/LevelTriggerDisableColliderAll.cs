using System.Collections.Generic;
using UnityEngine;


/*
 *  When invoked, finds and destroys all colliders on current gameobject
 *
 * JH
 */
public class LevelTriggerDisableColliderAll : MonoBehaviour, ITrigger
{
    public void Invoke()
    {
        new List<Collider>(GetComponents<Collider>()).ForEach(x => { x.enabled = false; });
        isCompleted = true;
    }

    public bool isCompleted { get; set; }
}