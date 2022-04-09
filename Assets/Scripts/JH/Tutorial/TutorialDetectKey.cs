using UnityEngine;


/*
 * Detects key used in the tutorial. Triggers external complete event
 * from LevelTriggerManager.
 *
 * JH
 */
public class TutorialDetectKey : MonoBehaviour
{
    private LevelTriggerManager _trigger;
    private PlayerInputActionAsset _input;
    [SerializeField] private bool detectJumpSprint;

    [SerializeField] private bool hasJumped;
    [SerializeField] private bool hasSprinted;

    private void OnEnable()
    {
        _input = FindObjectOfType<PlayerInputActionAsset>();
        _trigger = GetComponent<LevelTriggerManager>();
    }

    private void LateUpdate()
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
    }
}