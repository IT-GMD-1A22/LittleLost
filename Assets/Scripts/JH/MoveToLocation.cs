using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Moves object from one place to another, and return if set
 * Allows random start, wait time at destination.
 *
 * JH
 */

public class MoveToLocation : MonoBehaviour
{
    [SerializeField] private UpdateUsage selectedUpdateMethod = UpdateUsage.UPDATE;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector3 destinationFromCurrent;
    [SerializeField] private bool returnToStart = true;
    
    [SerializeField] private float delayAtDestination = 0f;
    [SerializeField] private bool delayAtDestinationRandom = true;
    
    [SerializeField] private bool randomStart;
    [SerializeField] private float randomStartRange = 3f;
    private float startDelay;
    private float _waitTimer;
    private Vector3 _currentDestination;
    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position;
        _currentDestination = _originalPosition + destinationFromCurrent;
        _waitTimer = delayAtDestination;
        startDelay = Random.Range(0f, randomStartRange);

    }

    // late update so movements can be "catched" by unity before actual movement is being done.
    private void LateUpdate()
    {
        if (selectedUpdateMethod == UpdateUsage.LATEUPDATE)
            updateMovement();
    }

    // fixed update when physics are involved
    private void FixedUpdate()
    {
        if (selectedUpdateMethod == UpdateUsage.FIXEDUPDATE)
            updateMovement();
    }

    // ordinary update
    private void Update()
    {
        if (selectedUpdateMethod == UpdateUsage.UPDATE)
            updateMovement();
    }

    private void updateMovement()
    {
        // delay start movement.
        if (DelayStart()) return;

        // move to target
        MoveToTarget();

        // delay check and return
        if (transform.position == _currentDestination && returnToStart)
        {
            if (_waitTimer > 0f)
            {
                _waitTimer -= Time.deltaTime;
            }
            else
            {
                SetNewCurrentDestination();
            }
        }
    }

    private void SetNewCurrentDestination()
    {
        _currentDestination = transform.position == _originalPosition
            ? _originalPosition + destinationFromCurrent
            : _originalPosition;
        _waitTimer = delayAtDestinationRandom ? Random.Range(0f, delayAtDestination) : delayAtDestination;
    }

    private void MoveToTarget()
    {
        if (transform.position != _currentDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentDestination, moveSpeed * Time.deltaTime);
        }
    }

    private bool DelayStart()
    {
        if (randomStart && startDelay > 0f)
        {
            startDelay -= Time.deltaTime;
            return true;
        }

        return false;
    }


    private enum UpdateUsage
    {
        UPDATE,
        FIXEDUPDATE,
        LATEUPDATE
    }
}
