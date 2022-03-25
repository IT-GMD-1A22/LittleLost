using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToObject : MonoBehaviour
{
    public GameObject cube;

    private void Update()
    {
        if (cube != null)
        {
            var parentTransform = transform.parent.transform;

            cube.transform.position = parentTransform.position;
            // cube.transform.rotation = parentTransform.rotation;
            cube.transform.rotation = parentTransform.rotation * new Quaternion(1f, 90f, 1f, 1f);

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cube = other.gameObject;
            cube.GetComponent<Animator>().SetBool("NoAnimation", true);
            cube.GetComponent<CharacterController>().enabled = false;
            cube.GetComponent<PlayerLaneChanger>().enabled = false;
        }
    }
}
