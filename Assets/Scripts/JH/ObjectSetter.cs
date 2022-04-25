using UnityEngine;

/*
 * Implementation of IObjectTransform.
 * Enables parenting gameobject when triggered.
 */
public class ObjectSetter : MonoBehaviour, IObjectTransform
{
    public void SetParent(Transform other)
    {
        other.parent = transform;
    }

    public void RemoveParent(Transform other)
    {
        other.parent = null;
    }
}