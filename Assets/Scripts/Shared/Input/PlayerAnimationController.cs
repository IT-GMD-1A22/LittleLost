using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private bool _hasAnimator;
    private Animator _animator;
    
    private float _animationBlend;
    
    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private int _animIDKill;


    private void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);

        AssignAnimationIDs();
    }
    
    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDKill = Animator.StringToHash("Kill");
    }


    public void SetGroundedAnimation(bool grounded)
    {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDGrounded, grounded);
        }
    }


    public void UpdateMovementAnimation(float inputMagnitude, float targetSpeed, float speedChangeRate)
    {
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, speedChangeRate);
        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
    }

    public void SetJumpAnimation(bool activated)
    {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDJump, activated);
        }
    }

    public void SetFallAnimation(bool activated)
    {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDFreeFall, activated);
        }
    }

    public void ResetFallJumpAnimation()
    {
        SetJumpAnimation(false);
        SetFallAnimation(false);
    }

    public void KillAnimation()
    {
        _animator.SetBool(_animIDKill, true);
    }
}
