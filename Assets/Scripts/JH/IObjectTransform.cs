using UnityEngine;

public interface IObjectTransform
{
    void SetParent(Transform other);
    void RemoveParent(Transform other);
}