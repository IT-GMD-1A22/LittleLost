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
        Debug.Log(Mathf.Round(transform.position.z / 3f) * 3f);
        
        _controller.GetInput().takeZMovement = allowLaneChange && !changing;
        if (allowLaneChange && !changing)
        {
            if (_controller.GetInput().move.y != 0.0f)
            {

                allowLaneChange = false;
                changing = true;
                originalZ = Mathf.Round(transform.position.z / 3f) * 3f;

                snapZ = Mathf.Round(_controller.GetInput().move.y > 0.0f
                    ? Mathf.Round(gameObject.transform.position.z + 3.0f)
                    : Mathf.Round(gameObject.transform.position.z - 3.0f));
            }
        }
        else if (changing)
        {
            var offset = Mathf.Clamp(snapZ - Mathf.Round(gameObject.transform.position.z), -1, 1); // normalize

            var hit = new RaycastHit();
            // check if player is hitting objects on the +-Z axis, if so, return to original position
            if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hit, 0.9f) && offset > 0.0f ||
                Physics.Raycast(gameObject.transform.position, -Vector3.forward, out hit, 0.9f) && offset < 0.0f)
            {
                snapZ = originalZ;
                _controller.GetInput().move.y = Mathf.Clamp(snapZ - gameObject.transform.position.z, -1, 1);
            }
            else if (Mathf.Abs(offset) > 0.1f)
            {
                // move the player towards the Z direction
                _controller.GetInput().move.y = offset;
            }
            else
            {
                _controller.GetInput().move.y = 0.0f;
                
                // fix as character controller resets transforms.position
                GetComponent<CharacterController>().enabled = false;
                
                // stop z movement and snap player to position for consistency.
                var snapPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Mathf.Round(transform.position.z / 3f) * 3f);
                gameObject.transform.position = snapPosition;
                
                // enable the character controller again to enable further movements
                GetComponent<CharacterController>().enabled = true;
                
                changing = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        allowLaneChange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        allowLaneChange = false;
    }
}