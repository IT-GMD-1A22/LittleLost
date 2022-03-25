using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDetectKey : MonoBehaviour
{
    private LevelTriggerManager _trigger;
    private PlayerInputActionAsset _input;
    [SerializeField] private bool detectJumpSprint;
    [SerializeField] private bool detectLaneChange;

    [SerializeField] private bool hasJumped;
    [SerializeField] private bool hasSprinted;
    [SerializeField] private bool hasZMoved;

    private void OnEnable()
    {
        _input = FindObjectOfType<PlayerInputActionAsset>();
        _trigger = GetComponent<LevelTriggerManager>();
    }


    private void Update()
    {
        if (detectJumpSprint)
        {
            if (!hasJumped && _input.jump)
            {
                hasJumped = true;
            }
            else if (!hasSprinted && _input.sprint)
            {
                hasSprinted = true;
            }
            else if (hasJumped && hasSprinted)
            {
                _trigger.CompleteEvent();
                detectJumpSprint = false;
            }
        }
        else if (detectLaneChange)
        {
            if (!hasZMoved && _input.move.y != 0f)
            {
                hasZMoved = true;
                detectLaneChange = false;
                _trigger.CompleteEvent();
            }
        }
    }
}