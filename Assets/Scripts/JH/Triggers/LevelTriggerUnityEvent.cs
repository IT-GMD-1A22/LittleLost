using System.Collections;
using UnityEngine;
using UnityEngine.Events;


/*
 * Invokes unity events when triggered
 *
 * -jh
 */
[RequireComponent(typeof(LevelTriggerManager))]
public class LevelTriggerUnityEvent : MonoBehaviour, ITrigger
{
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent unityEvent;
    public bool isCompleted { get; set; }

    private  IEnumerator RunEvent()
    {
        yield return new WaitForSeconds(delay);
        unityEvent.Invoke();
        isCompleted = true;
    }

    public void Invoke()
    {
        StartCoroutine(RunEvent());
    }
}