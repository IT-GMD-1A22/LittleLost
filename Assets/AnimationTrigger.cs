using System;
using System.Collections;
using UnityEngine;

/*
 * When triggers, enable tigger animation
 * if enabled, also triggers player animation
 *
 * JH
 */
public class AnimationTrigger : MonoBehaviour
{
    private SpawnManager _spawn;
    private LevelAudioPlayer _audio;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private bool triggerPlayerAnimation;

    private void Awake()
    {
        _spawn = FindObjectOfType<SpawnManager>();
        _audio = FindObjectOfType<LevelAudioPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartDeathRoutine(other.gameObject));
        }
    }

    private IEnumerator StartDeathRoutine(GameObject player)
    {
        var animController = player.GetComponent<PlayerAnimationController>();
        var input = player.GetComponent<PlayerInputActionAsset>();
        
        input.move = Vector2.zero;
        input.enableInput = false;
        
        _animator.SetBool("Trigger", true);
        _audio.PlayClipWithTag("dying");

        if (triggerPlayerAnimation)
        {
            animController.KillAnimation();
            
            yield return new WaitForSeconds(4f);
            _spawn.SpawnPlayer();
        }
        
        _animator.SetBool("Trigger", false);
        
    }

}
