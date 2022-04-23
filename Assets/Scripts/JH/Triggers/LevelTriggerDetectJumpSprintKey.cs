using UnityEngine;


/*
 * Detects key used in the tutorial. Triggers external complete event
 * from LevelTriggerManager.
 *
 * JH
 */
public class LevelTriggerDetectJumpSprintKey : MonoBehaviour, ITrigger
{
    private PlayerInputActionAsset _input;
    [SerializeField] private bool detectJumpSprint;
    [SerializeField] private bool hasJumped;
    [SerializeField] private bool hasSprinted;

    public bool isCompleted { get; set; }

    public void Invoke()
    {
        detectJumpSprint = true;
    }

    private void OnEnable()
    {
        _input = FindObjectOfType<PlayerInputActionAsset>();
    }

    private void Update()
    {
        if (!detectJumpSprint) return;
        CheckIfComplete();
        CheckForJump();
        CheckForSprint();
    }

    private void CheckForJump()
    {
        if (hasJumped || !_input.jump) return;
        hasJumped = true;
    }

    private void CheckForSprint()
    {
        if (hasSprinted || !_input.sprint) return;
        hasSprinted = true;
    }

    private void CheckIfComplete()
    {
        if (!hasJumped || !hasSprinted) return;

        detectJumpSprint = false;
        isCompleted = true;
    }
}