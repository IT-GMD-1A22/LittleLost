using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Triggers all ITriggers attached to gameobject
 * when activated defined by triggerEvent.
 *
 * Event scripts has to contain ITrigger interface
 * to be able to be invoked.
 * JH
 */
public class LevelTriggerManager : MonoBehaviour
{
    [SerializeField] private TriggerEvent _triggerEvent;
    [SerializeField] private string colliderTag = "Player";
    [SerializeField] private bool waitForTriggerCompleteBetweenEachTrigger;
    private List<ITrigger> triggers;

    private void Awake()
    {
        triggers = new List<ITrigger>(GetComponents<ITrigger>());
    }

    private IEnumerator ActivateTriggers()
    {
        if (waitForTriggerCompleteBetweenEachTrigger)
        {
            foreach (var trigger in triggers)
            {
                trigger.Invoke();
                yield return new WaitUntil(() => trigger.isCompleted);
            }
        }
        else
        {
            triggers.ForEach(t => t.Invoke());
        }
    }

    private bool AllowTriggers(Collider other)
    {
        if (!string.IsNullOrWhiteSpace(colliderTag) && other.CompareTag(colliderTag))
        {
            return true;
        }

        return string.IsNullOrWhiteSpace(colliderTag);
    }

    private void Start()
    {
        if (_triggerEvent != TriggerEvent.START) return;
        StartCoroutine(ActivateTriggers());
    }

    private void OnEnable()
    {
        if (_triggerEvent != TriggerEvent.ENABLED) return;
        StartCoroutine(ActivateTriggers());
    }

    private void OnDisable()
    {
        if (_triggerEvent != TriggerEvent.DISABLED) return;
        StartCoroutine(ActivateTriggers());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_triggerEvent != TriggerEvent.TRIGGER_ENTER) return;
        if (!AllowTriggers(other)) return;
        StartCoroutine(ActivateTriggers());
    }

    private void OnTriggerStay(Collider other)
    {
        if (_triggerEvent != TriggerEvent.TRIGGER_STAY) return;
        if (!AllowTriggers(other)) return;
        StartCoroutine(ActivateTriggers());
        ;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_triggerEvent != TriggerEvent.TRIGGER_EXIT) return;
        if (!AllowTriggers(other)) return;

        StartCoroutine(ActivateTriggers());
    }
}