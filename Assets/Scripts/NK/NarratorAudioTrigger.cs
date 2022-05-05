using UnityEngine;

/*
 * Script that triggers the narrator TTS clip when entered.
 * - NK
 */
public class NarratorAudioTrigger : MonoBehaviour
{
    [SerializeField] private int colliderId = 0;
    [SerializeField] private bool playOnce = true;
    [SerializeField] private bool playMultiple = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playOnce)
            {
                // Utilizes the singleton instance of the Narrator Sound Manager
                NarratorSoundManager.Instance.PlaySoundClip(colliderId);
                playOnce = false;
            }

            if (playMultiple)
            {
                NarratorSoundManager.Instance.PlaySoundClip(colliderId);
            }
        }
    }
}
