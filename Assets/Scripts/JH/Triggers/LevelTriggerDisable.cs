using System.Collections;
using UnityEngine;

/*
 * Disables game object when triggered
 */
public class LevelTriggerDisable : LevelTriggerBase
{
    protected override IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        _object.SetActive(false);
        isCompleted = true;
    }
}