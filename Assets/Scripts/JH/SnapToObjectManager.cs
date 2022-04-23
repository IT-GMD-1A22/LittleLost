using System;
using UnityEngine;

/*
 * Used to snap player to plane.
 * Can also unsnap when player arrive at destination
 *
 * JH
 */
public class SnapToObjectManager : MonoBehaviour
{
    private ISnapper _snap;
    public bool shouldSnap;


    private void Awake()
    {
        _snap = GetComponent<Snapper>();
    }

    private void Update()
    {
        _snap?.SetSnapPosition();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!AllowSnap(other)) return;

        _snap.Snap(other);
    }

    private bool AllowSnap(Collider other)
    {
        if (other.CompareTag("Player") && shouldSnap)
            return true;
        return false;
    }
}