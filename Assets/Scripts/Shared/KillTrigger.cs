using System;
using System.Collections;
using UnityEngine;

/*
 * Kills the player and respawns it at the latest respawn beacon.
 * JH
 */
public class KillTrigger : MonoBehaviour
{
    private SpawnManager _spawn;
    private LevelAudioPlayer _audio;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private string localAnimatorTriggerName;
    [SerializeField] private bool triggerPlayerAnimation;
    [SerializeField] private float waitBeforeSpawn = 4f;
    [SerializeField] private string audioTagToPlay = "dying";
    [ReadOnly, SerializeField] private bool runningRoutine = false;
    
    private void Awake()
    {
        _spawn = FindObjectOfType<SpawnManager>();
        _audio = FindObjectOfType<LevelAudioPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !runningRoutine)
        {
            Debug.Log("triggered");
            runningRoutine = true;
            StartCoroutine(StartDeathRoutine(other.gameObject));
        }
    }

    private IEnumerator StartDeathRoutine(GameObject player)
    {
        var animController = player.GetComponent<PlayerAnimationController>();
        var controller = player.GetComponent<PlayerController>();
        controller.disablePlayerInput = true;
        
        if (_animator && !string.IsNullOrEmpty(localAnimatorTriggerName)) _animator.SetBool(localAnimatorTriggerName, true);
        _audio.PlayClipWithTag(audioTagToPlay);

        if (triggerPlayerAnimation)
        {
            animController.KillAnimation(true);
            
            yield return new WaitForSeconds(waitBeforeSpawn);
            _spawn.SpawnPlayer();
        }
        
        if(_animator && !string.IsNullOrEmpty(localAnimatorTriggerName))_animator.SetBool(localAnimatorTriggerName, false);
        runningRoutine = false;
    }

}
