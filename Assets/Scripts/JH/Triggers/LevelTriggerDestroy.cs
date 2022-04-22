using System.Collections;
using UnityEngine;

/*
 * 
 */
[RequireComponent(typeof(LevelTriggerManager))]
public class LevelTriggerDestroy : LevelTriggerBase
{
    protected override IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        Destroy(_object);
        isCompleted = true;
    }
}