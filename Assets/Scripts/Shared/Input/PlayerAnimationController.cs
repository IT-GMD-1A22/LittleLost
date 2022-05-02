using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Controls player animations
 *
 * JH
 */
public class PlayerAnimationController : MonoBehaviour
{
    private bool _hasAnimator;
    private Animator _animator;
    
    private float _animationBlend;
    [SerializeField] private int numberOfDeathAnimations = 5;
    
    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private int _animIDKillTrigger;
    private int _animIDKill;
    private int _animIDClimbing;


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
        _animIDKillTrigger = Animator.StringToHash("KillTrigger");
        _animIDKill = Animator.StringToHash("Kill");
        _animIDClimbing = Animator.StringToHash("Climbing");
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

    public void KillAnimation(bool random, int killAnimation = 0)
    {
        var killAnim = killAnimation;
        if (random)
        {
            killAnim = Random.Range(0, numberOfDeathAnimations);
        }
        _animator.SetInteger(_animIDKill ,killAnim);
        _animator.SetTrigger(_animIDKillTrigger);
    }

    public void SetClimbAnimation(bool activated)
    {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDClimbing, activated);
        }
    }
}
