using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventAnimatePause : MonoBehaviour
{
    [SerializeField] private string playEventName = "";
    [SerializeField] private string pauseEventName = "";
    [SerializeField] private bool animateOnAwake = false;

    private Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
        if (animateOnAwake)
            Play();
        else 
            Pause();
    }

    void OnEnable ()
    {
        EventManager.StartListening (playEventName, Play);
        EventManager.StartListening (pauseEventName, Pause);
    }

    void OnDisable ()
    {
        EventManager.StopListening (playEventName, Play);
        EventManager.StopListening (pauseEventName, Pause);
    }

    private void Pause() {
        if (_animator)
            _animator.enabled = false;
    }

    private void Play() {
        if (_animator) 
            _animator.enabled = true;
    }
}
