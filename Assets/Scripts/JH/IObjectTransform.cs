using UnityEngine;

/*
 * Interface to expose transform handling
 */
public interface IObjectTransform
{
    void SetParent(Transform other);
    void RemoveParent(Transform other);
}