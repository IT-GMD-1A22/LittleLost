using System;
using UnityEngine;

namespace AS
{
    public class TeleportScript : MonoBehaviour
    {
        [SerializeField] private Vector3 teleportTarget;
        private void OnTriggerEnter(Collider other)
        {
            // Set player position to target position
            other.transform.position = teleportTarget;
        }
    }
}
