using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeriodicDisablerScript : MonoBehaviour
{
    [Range(0,10)][SerializeField] private float timeRange;
    [SerializeField] private List<GameObject> _objects;
    private float countdown;

    private void Awake()
    {
        countdown = timeRange;
    }

    private void Update()
    {
        if (countdown <= 0f)
        {
            _objects.ForEach(x => x.SetActive(!x.activeSelf));
            countdown = timeRange;
        }
        countdown -= Time.deltaTime;
    }
    
}
