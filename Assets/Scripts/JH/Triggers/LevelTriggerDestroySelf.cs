using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LevelTriggerManager))]
public class LevelTriggerDestroySelf : MonoBehaviour, ITrigger
{
    [SerializeField] private float delay;
    
    private IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        isCompleted = true;
    }

    public void Invoke()
    {
        StartCoroutine(RunEvent());
    }

    public bool isCompleted { get; set; }
}