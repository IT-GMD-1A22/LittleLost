using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LadderUtil : MonoBehaviour
{

    private PlayerController _controller;
    private CharacterController _characterController;
    private PlayerLaneChanger _playerLaneChanger;
    // TODO Change to private after bug fixing
    public bool _active = false;
    public bool _top = false;
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
        _top = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            _playerInput.enabled = false;
            _active = true;
            ladder = other.transform;
        }

        if (other.CompareTag("LadderTop"))
        {
            _playerInput.enabled = false;
            _top = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder") || other.CompareTag("LadderTop"))
        {
            _playerInput.enabled = true;
            _active = false;
            _top = false;
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
                // TODO Figure out why I only can climb the ladder once when the lane changer is active
                // _playerLaneChanger.enabled = true;
        
        
            }
    
        }

        if (_top)
        {
            Debug.Log(transform.position);
            var destVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Vector3 diffVector = Vector3.MoveTowards(destVector, new Vector3(20f, 0f, 20f), _controller.GetVelocity().magnitude * Time.deltaTime);
            transform.position = diffVector;
            Debug.Log(diffVector);
        }
    }

    // private void LateUpdate()
    // {
    //     if (_active == true && controller.GetInput().move.y > 0)
    //     {
    //         // GetComponent<CharacterController>().enabled = false;
    //         var destVector = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    //         Vector3 diffVector = Vector3.MoveTowards(transform.position, destVector, ladderSpeed * Time.deltaTime);
    //         transform.position = new Vector3(ladder.position.x, diffVector.y, ladder.position.z);
    //         // GetComponent<CharacterController>().enabled = true;
    //     }
    //
    //     // if (_active == true && Input.GetKey(KeyCode.S))
    //     // {
    //     //     transform.position += Vector3.down * upDownSpeed * Time.deltaTime;
    //     // }
    // }
}
