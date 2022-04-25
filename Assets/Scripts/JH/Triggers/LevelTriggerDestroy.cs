using System.Collections;
using UnityEngine;

/*
 *   Destroy a game object when triggered
 */
public class LevelTriggerDestroy : LevelTriggerBase
{
    protected override IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        Destroy(_object);
        isCompleted = true;
    }
}