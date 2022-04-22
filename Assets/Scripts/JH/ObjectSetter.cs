using UnityEngine;

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