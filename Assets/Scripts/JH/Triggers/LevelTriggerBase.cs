using System;
using System.Collections;
using UnityEngine;

/*
 * Base class for ITrigger implementations
 * JH
 */
public abstract class LevelTriggerBase : MonoBehaviour, ITrigger
{
    [SerializeField] internal GameObject _object;
    [SerializeField] internal float delay = 0f;

    protected abstract IEnumerator RunEvent();
    public void Invoke()
    {
        StartCoroutine(RunEvent());
    }

    public bool isCompleted { get; set; }
}