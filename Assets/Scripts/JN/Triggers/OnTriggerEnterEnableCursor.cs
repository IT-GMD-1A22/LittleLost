using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterEnableCursor : MonoBehaviour
{
    [SerializeField] private bool enableCursorOnEvent = true;
    [SerializeField] private float delay = 0.0f;
    
    private PlayerInputActionAsset _input;

    private IEnumerator SetCursor(Collider other)
    {
        yield return new WaitForSeconds(delay);
        if (other.CompareTag("Player")) {
            _input = other.GetComponent<PlayerInputActionAsset>();
            _input.cursorLocked = !enableCursorOnEvent;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SetCursor(other));
    }
}
