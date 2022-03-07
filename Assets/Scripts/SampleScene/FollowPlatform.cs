using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Allows player to follow gameobject, like platforms while moving.
 * JH
 */

public class FollowPlatform : MonoBehaviour
{

    CharacterController _controller;
    Vector3 _moveDirection;
    Vector3 _globalPosition;
    Vector3 _localPosition;
    [SerializeField] private bool enabled = true;

    // Update is called once per frame
    void Update()
    {
        if (_controller != null && enabled)
        {
            Vector3 newGlobalPlatformPoint = transform.TransformPoint(_localPosition);
            _moveDirection = newGlobalPlatformPoint - _globalPosition;
            if (_moveDirection.magnitude > 0.01f)
            {
                _controller.Move(_moveDirection);
            }

            var position = transform.position;
            _globalPosition = position;
            _localPosition = transform.InverseTransformPoint(position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _controller = other.GetComponent<CharacterController>();
        var position = transform.position;
        _globalPosition = position;
        _localPosition = transform.InverseTransformPoint(position);
    }

    private void OnTriggerExit(Collider other)
    {
        _controller = null;
    }
    
}