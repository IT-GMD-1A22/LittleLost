using UnityEngine;

public interface ISnapper
{
    void Snap(Collider other);
    void UnSnapPlayer();

    void SetSnapPosition();
}