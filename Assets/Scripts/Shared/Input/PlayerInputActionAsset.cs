using UnityEngine;
using UnityEngine.InputSystem;


/*
 *  Modified from Standard Assets third person character controller
 *  for LittleLost usage.
 * JH
 */

public class PlayerInputActionAsset : MonoBehaviour
{
    [Header("Character Input Values")] 
    public Vector2 move;
    public bool jump;
    public bool sprint;
    [Header("Movement Settings")] public bool analogMovement;
    public bool takeZMovement = true;
#if !UNITY_IOS || !UNITY_ANDROID
    [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
    public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
            MoveInput(value.Get<Vector2>());
		}
        

		public void OnJump(InputValue value)
		{
            JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
            SprintInput(value.isPressed);
		}
#endif


    public void MoveInput(Vector2 newMoveDirection)
    {
        if (!takeZMovement)
        {
            // ensures no z movement is allowed unless specified
            newMoveDirection.y = 0.0f;
        }

        move = newMoveDirection;
    }
    
    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

#if !UNITY_IOS || !UNITY_ANDROID

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

#endif
}