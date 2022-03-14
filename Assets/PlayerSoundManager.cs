using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Script to manage player sounds.
 * Script is inspired by https://www.youtube.com/watch?v=Bnm8mzxnwP8
 *
 * note: animation event dictates when a sound is being played with the below functions i.e. step, hop.
 * JH
 */
[RequireComponent(typeof(AudioSource))]
public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] stepClips;
    [SerializeField] private AudioClip[] hopClips;
    [SerializeField] private Animator _animator;

    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        
    }

    private void Step()
    {
        if (_animator.GetFloat("Speed") > 0.1f)
        {
            AudioClip clip = GetRandomStepClip();
            _audioSource.PlayOneShot(clip);
        }
        else
        {
            _audioSource.Stop();
        }
        
    }

    private void Hop()
    {
        AudioClip clip = GetRandomHopClip();
        
        _audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomHopClip()
    {
        return hopClips[Random.Range(0, hopClips.Length)];
    }

    private AudioClip GetRandomStepClip()
    {
        return stepClips[Random.Range(0, stepClips.Length)];
    }
    
    
    
    
}
