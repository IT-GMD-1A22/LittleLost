using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Created with guidance and inspiration from JH's 'PlayerController'.
 *
 * Script to handle the character when climbing the wire
 * - NK
 */

public class LadderUtil : MonoBehaviour
{
    private PlayerController _controller;
    private CharacterController _characterController;
    private PlayerLaneChanger _playerLaneChanger;
    [SerializeField] public bool _active = false;
    [SerializeField] private float ladderSpeed = 3.2f;
    private PlayerInputActionAsset _playerInput;
    
    private Transform ladder;

    
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInputActionAsset>();
        _controller = GetComponent<PlayerController>();
        _characterController = GetComponent<CharacterController>();
        _playerLaneChanger = GetComponent<PlayerLaneChanger>();
        _active = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder") || other.CompareTag("LadderTop"))
        {
            _playerInput.enabled = false;
            _active = true;
            ladder = other.transform;
            _playerInput.takeZMovement = true;
            _playerLaneChanger.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder") || other.CompareTag("LadderTop"))
        {
            _playerInput.enabled = true;
            _active = false;
            _playerInput.takeZMovement = false;
            _playerLaneChanger.enabled = true;
        }
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (_active) {
            _playerLaneChanger.enabled = false;
            var destVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            var moveVector = Vector3.zero;
            if (_controller.GetInput().move.y > 0) {
                Vector3 diffVector = Vector3.MoveTowards(transform.position, destVector + new Vector3(0f, 0.5f, 0f), ladderSpeed * Time.deltaTime);
                moveVector = new Vector3(ladder.position.x, diffVector.y, ladder.position.z);
            }
            else if (_controller.GetInput().move.y < 0) {
                Vector3 diffVector = Vector3.MoveTowards(transform.position, destVector - new Vector3(0f, 0.5f, 0f), ladderSpeed * Time.deltaTime);
                moveVector = new Vector3(ladder.position.x, diffVector.y, ladder.position.z);
            }
        
            if (moveVector != Vector3.zero)
            {
                _characterController.enabled = false;
                _playerLaneChanger.enabled = false;
                transform.position = moveVector;
                _characterController.enabled = true;
                _playerLaneChanger.enabled = true;
            }
    
        }
    }
}
