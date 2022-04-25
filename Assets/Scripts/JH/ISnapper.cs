using UnityEngine;


/*
 * Interface to expose snapping feature
 */
public interface ISnapper
{
    void Snap(Collider other);
    void UnSnapPlayer();

    void SetSnapPosition();
}