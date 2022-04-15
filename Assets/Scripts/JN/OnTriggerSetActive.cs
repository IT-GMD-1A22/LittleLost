using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnTriggerSetActive : MonoBehaviour
{
    [SerializeField] private bool setActive = false;
    [SerializeField] private List<GameObject> objects;

    [SerializeField] private bool useColliderTags = false;
    [SerializeField] private List<string> colliderTags;
    [SerializeField] private bool setOnce = false;
    private int _setCount = 0;

    private void SetObjects() {
        if (objects.Any()) {
            objects.ForEach(o => o.SetActive(setActive));
            _setCount ++;
        }
    }

    private bool TagsMatch(string tag) {
        if (colliderTags.Any() && colliderTags.Contains(tag)) {
            return true;
        }
        else {
            return false;
        }
    }

    private bool CanSet() {
        if (setOnce) {
            return _setCount == 0 ? true : false;
        }
        else {
            return true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CanSet()) {
            if (useColliderTags) {
                if (TagsMatch(other.tag)) {
                    SetObjects();
                }
            }
            else {
                SetObjects();
            }
        }
    }
}
