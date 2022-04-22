using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LevelTriggerManager))]
public class LevelTriggerDisable : LevelTriggerBase
{
    protected override IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        _object.SetActive(false);
        isCompleted = true;
    }
}