using System.Collections;
using UnityEngine;

namespace AS
{
    public class KillScript : MonoBehaviour
    {
        private SpawnManager _spawn;
        private LevelAudioPlayer _audio;
    
        [SerializeField] private bool triggerPlayerAnimation = true;
        [SerializeField] private float waitBeforeSpawn = 4f;
        [SerializeField] private string audioTagToPlay = "dying";
        [ReadOnly, SerializeField] private bool runningRoutine;

        // Initializes _spawn and _audio before the application starts
        private void Awake()
        {
            _spawn = FindObjectOfType<SpawnManager>();
            _audio = FindObjectOfType<LevelAudioPlayer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || runningRoutine)
                return;
        
            runningRoutine = true;
            StartCoroutine(StartDeathRoutine(other.gameObject));
        }

        private IEnumerator StartDeathRoutine(GameObject player)
        {
            var animController = player.GetComponent<PlayerAnimationController>();
            var controller = player.GetComponent<PlayerController>();
            controller.disablePlayerInput = true;
        
            _audio.PlayClipWithTagInterruptFirst(audioTagToPlay);

            if (triggerPlayerAnimation)
            {
                animController.KillAnimation(true);
            
                yield return new WaitForSeconds(waitBeforeSpawn);
                _spawn.SpawnPlayer();
            }
        
            runningRoutine = false;
        }
    }
}
