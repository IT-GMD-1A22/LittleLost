using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LevelTriggerManager))]
public class LevelTriggerEnable : LevelTriggerBase
{
    protected override IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        _object.SetActive(true);
        isCompleted = true;
    }
}