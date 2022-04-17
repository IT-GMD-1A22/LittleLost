using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform destination;
    // [SerializeField] private float delayPortal = 1f;
    public GameObject Player;

    // private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        // _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // _characterController.enabled = false;
            // yield return new WaitForSeconds(delayPortal);
            Player.transform.position = destination.transform.position;
            // _characterController.enabled = true;
        }
    }
}
