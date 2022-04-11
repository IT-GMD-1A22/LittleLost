using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FausetHandle : MonoBehaviour
{

    private Animator _animator;
    private readonly int _animIdOpen = Animator.StringToHash("Open");

    private enum _function {Open, Close};

    [SerializeField] private GameObject flowObj;

    [SerializeField] private _function performs = _function.Open;

    private void Awake() {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (performs == _function.Open) {
            if (!_animator.GetBool(_animIdOpen)) {
                _animator.SetBool(_animIdOpen, true);
                if (flowObj)
                    flowObj.GetComponent<ParticleSystem>().Play();
            }
        }
        else {
            if (_animator.GetBool(_animIdOpen)) {
                _animator.SetBool(_animIdOpen, false);
                if (flowObj)
                    flowObj.GetComponent<ParticleSystem>().Stop();
            }

        }
    }

}
