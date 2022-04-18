using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventControlFauset : MonoBehaviour
{
    [SerializeField] private string openRightEventName = "";
    [SerializeField] private string closeRightEventName = "";
    [SerializeField] private string openLeftEventName = "";
    [SerializeField] private string closeLeftEventName = "";

    private ParticleSystem _flow;
    private bool _rightOpen = true;
    private bool _leftOpen = false;

    void Awake() 
    {
        _flow = gameObject.GetComponent<ParticleSystem>();
    }

    void OnEnable ()
    {
        EventManager.StartListening (openRightEventName, OpenRight);
        EventManager.StartListening (closeRightEventName, CloseRight);
        EventManager.StartListening (openLeftEventName, OpenLeft);
        EventManager.StartListening (closeLeftEventName, CloseLeft);
    }

    void OnDisable ()
    {
        EventManager.StopListening (openRightEventName, OpenRight);
        EventManager.StopListening (closeRightEventName, CloseRight);
        EventManager.StopListening (openLeftEventName, OpenLeft);
        EventManager.StopListening (closeLeftEventName, CloseLeft);
    }

    private void OpenRight() {
        _rightOpen = true;
        SetParticleSystem();
    }

    private void CloseRight() {
        _rightOpen = false;
        SetParticleSystem();
    }

    private void OpenLeft() {
        _leftOpen = true;
        SetParticleSystem();
    }

    private void CloseLeft() {
        _leftOpen = false;
        SetParticleSystem();
    }

    private void SetParticleSystem() {
        if (_flow) {
            if (_rightOpen || _leftOpen) {
                _flow.Play();
            }
            else {
                _flow.Stop();
            }
        }
        else 
        {
            Debug.Log("A particle system must be attached to the game object.");
        }
    }
}
