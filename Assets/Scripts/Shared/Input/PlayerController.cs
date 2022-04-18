using System;
using UnityEngine;
using UnityEngine.InputSystem;


/*  Node: Modified version of Standard assets third person character controller.
 *        modified for usage to 2.5D.
 *  JH
 */

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player")] [Tooltip("Move speed of the character in m/s")]
    public float moveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float sprintSpeed = 5.335f;

    [Tooltip("How fast the character turns to face movement direction")] [Range(0.0f, 0.3f)]
    public float rotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Space(10)] [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;

    [Space(10)] [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")] public float GroundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;


    // player
    private float _speed;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private Vector2 inputMove;
    private bool inputJump;
    private bool inputSprint;
    public bool disablePlayerInput;


    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private PlayerAnimationController _animController;
    private bool _hasAnimController;

    private CharacterController _controller;
    private PlayerInputActionAsset _inputActionAsset;
    private GameObject _mainCamera;


    private void Start()
    {
        _hasAnimController = TryGetComponent(out _animController);

        _controller = GetComponent<CharacterController>();
        _inputActionAsset = GetComponent<PlayerInputActionAsset>();
        _mainCamera = GameObject.Find("Main Camera");

        // reset our timeouts on start
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    private void Update()
    {
        /* *** Modified by NK ***
         * Using the if statement to check whether the game is paused.
         *      True = breaks out of the event, such the player does not have any functionality.
         *      False = Normal player behaviour.
         */
        if (PauseMenuManager.isPaused) return;
        PlayerMovementCheck();
        JumpAndGravity();
        GroundedCheck();
        Move();
    }

    private void PlayerMovementCheck()
    {
        if (disablePlayerInput)
        {
            // disables input by setting a temporary variable as placeholder.
            inputMove = Vector2.zero;
            inputJump = false;
            inputSprint = false;
        }
        else
        {
            inputMove = _inputActionAsset.move;
            inputJump = _inputActionAsset.jump;
            inputSprint = _inputActionAsset.sprint;
        }
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        var trans = transform.position;
        Vector3 spherePosition = new Vector3(trans.x, trans.y - GroundedOffset,
            trans.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        if (_hasAnimController)
            _animController.SetGroundedAnimation(Grounded);
    }


    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = inputSprint ? sprintSpeed : moveSpeed;

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (inputMove == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        var vel = _controller.velocity;
        float currentHorizontalSpeed = new Vector3(vel.x, 0.0f, vel.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _inputActionAsset.analogMovement ? inputMove.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            
            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }


        // normalise input direction
        Vector3 inputDirection = new Vector3(inputMove.x, 0.0f, inputMove.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (inputMove != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                rotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        // move the player or apply external force
        if (externalForce.magnitude > sprintSpeed)
        {
            _controller.Move(new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime +
                             externalForce * Time.deltaTime);
            externalForce = Vector3.Lerp(externalForce, Vector3.zero, 5f * Time.deltaTime);
        }
        else
        {

            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }

        // update movement animations
        if (_hasAnimController)
            _animController.UpdateMovementAnimation(inputMagnitude, targetSpeed, Time.deltaTime * SpeedChangeRate);
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            if (_hasAnimController)
                _animController.ResetFallJumpAnimation();

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (inputJump && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                // update animator if using character
                if (_hasAnimController)
                    _animController.SetJumpAnimation(true);
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator if using character
                if (_hasAnimController)
                    _animController.SetFallAnimation(true);
            }

            // if we are not grounded, do not jump
            _inputActionAsset.jump = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    public PlayerInputActionAsset GetInput()
    {
        return _inputActionAsset;
    }

    private Vector3 externalForce;

    public void AddExternalForce(Vector3 direction, float force)
    {
        direction.Normalize();
        externalForce += direction.normalized * force / 3f; //3f=mass fictives
    }

    public void TeleportPlayerToVector(Vector3 location)
    {
        _controller.enabled = false;
        transform.position = location;
        _controller.enabled = true;
    }
    
    public Vector3 GetVelocity()
    {
        if (_controller == null)
            return Vector3.zero;
        return _controller.velocity;
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        var trans = transform.position;
        Gizmos.DrawSphere(
            new Vector3(trans.x, trans.y - GroundedOffset, trans.z),
            GroundedRadius);
    }
}