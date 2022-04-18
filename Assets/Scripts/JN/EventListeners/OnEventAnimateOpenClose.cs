using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventAnimateOpenClose : MonoBehaviour
{
    [SerializeField] private string openOnEventName = "";
    [SerializeField] private string closeOnEventName = "";
    [SerializeField] private static string animatorBoolName = "Open";
    [SerializeField] private bool animateOpenOnTrue = true;

    private readonly int _animIdOpen = Animator.StringToHash(animatorBoolName);

    private Animator _animator;

    void Awake() {
        _animator = GetComponent<Animator>();
    }

    void OnEnable ()
    {
        EventManager.StartListening (openOnEventName, Open);
        EventManager.StartListening (closeOnEventName, Close);
    }

    void OnDisable ()
    {
        EventManager.StopListening (openOnEventName, Open);
        EventManager.StopListening (closeOnEventName, Close);
    }

    private void Close() {
        if (_animator.GetBool(_animIdOpen))
            _animator.SetBool(_animIdOpen, !animateOpenOnTrue);
    }

    private void Open() {
        if (!_animator.GetBool(_animIdOpen)) 
            _animator.SetBool(_animIdOpen, animateOpenOnTrue);
    }
}
