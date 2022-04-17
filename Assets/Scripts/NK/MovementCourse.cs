using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MovementCourse : MonoBehaviour
{
    [SerializeField] private Transform moveFromPosition, moveToPosition;
    [SerializeField] private float movementSpeed = 1f;
    private Vector3 _currentDestination;
    
    private void Start()
    {
        _currentDestination = moveToPosition.position;
    }
    
    private void FixedUpdate()
    {
        if (transform.position == moveFromPosition.position)
        {
            _currentDestination = moveToPosition.position;
        } else if (transform.position == moveToPosition.position)
        {
            _currentDestination = moveFromPosition.position;
        }
    
        transform.position = Vector3.MoveTowards(transform.position, _currentDestination, movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
