using System;
using UnityEngine;

/*
 * Allows player to change playing lane.
 * Disable controls while chaning lanes, but allows jump while
 * JH
 */

[RequireComponent(typeof(PlayerController))]
public class PlayerLaneChanger : MonoBehaviour
{ 
    private PlayerController _controller;
    [SerializeField] private bool allowLaneChange;

    [SerializeField] private bool allowChangeZPlus;
    [SerializeField] private bool allowChangeZMinus;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }

    [SerializeField] private bool changing;
    [SerializeField] private float snapZ;

    private float originalZ;

    // Late update to ensure that update from playercontroller has been applied (Fixed -> update -> lateupdate)
    private void LateUpdate()
    {
        _controller.GetInput().takeZMovement = allowLaneChange && !changing;
        var transformPosition = transform.position;
        if (allowLaneChange && !changing)
        {
            if (_controller.GetInput().move.y != 0.0f)
            {
                allowLaneChange = false;
                changing = true;
                originalZ = Mathf.Round(transformPosition.z / 3f) * 3f;

                snapZ = Mathf.Round(_controller.GetInput().move.y > 0.0f
                    ? Mathf.Round(transformPosition.z + 3.0f)
                    : Mathf.Round(transformPosition.z - 3.0f));
            }
        }
        else if (!changing && !allowLaneChange)
        {
            // lock character to Z positon unless we allow to change it
            transform.position = new Vector3(transformPosition.x, transformPosition.y, Mathf.Round(transformPosition.z / 3f) * 3f);
        }
        else if (changing)
        {
            // find move direction
            var offset = Mathf.Clamp(snapZ - Mathf.Round(transformPosition.z), -1, 1);
            // find destination vector of new lane
            var destVector = new Vector3(transformPosition.x, transformPosition.y, snapZ);
            // find difference from current to dest
            Vector3 diffVector = Vector3.MoveTowards(transformPosition, destVector, _controller.GetVelocity().magnitude * Time.deltaTime);
            // force position to new vector
            transform.position = diffVector;
            // tell player controller we are moving
            _controller.GetInput().move.y = offset;
            // create raycast hit object
            var hit = new RaycastHit();
            // check if we are hitting something when switching lane
            if (Physics.Raycast(transformPosition, Vector3.forward, out hit, 0.9f) && offset > 0.0f ||
                Physics.Raycast(transformPosition, -Vector3.forward, out hit, 0.9f) && offset < 0.0f)
            {
                // if we hit something, move back to original lane by setting snapZ
                snapZ = originalZ;
                // tell player controller we are moving in direction
                _controller.GetInput().move.y = Mathf.Clamp(snapZ - transformPosition.z, -1, 1);
            }
            else if (Math.Abs(transformPosition.z - snapZ) < 0.1f)
            {
                // check if we are at goal destination, if so, disable the movement and tell
                // player controller top stop move.
                changing = false;
                _controller.GetInput().move.y = 0.0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AllowLaneChange"))
            allowLaneChange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AllowLaneChange"))
            allowLaneChange = false;
    }
}