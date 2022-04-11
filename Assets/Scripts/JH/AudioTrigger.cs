using UnityEngine;


/*
 * Plays defined audio on trigger
 *
 * JH
 */
public class AudioTrigger : MonoBehaviour
{
    private LevelAudioPlayer _levelAudioPlayer;
    [SerializeField] private AudioClip triggerSound;
    private void Awake()
    {
        _levelAudioPlayer = FindObjectOfType<LevelAudioPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _levelAudioPlayer.AddClipToQueue(triggerSound);
    }
}