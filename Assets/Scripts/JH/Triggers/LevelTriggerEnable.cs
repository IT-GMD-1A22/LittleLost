using System.Collections;
using UnityEngine;

/*
 * Enables game object when triggered
 */
public class LevelTriggerEnable : LevelTriggerBase
{
    protected override IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        _object.SetActive(true);
        isCompleted = true;
    }
}