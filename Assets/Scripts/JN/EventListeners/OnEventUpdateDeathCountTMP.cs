using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnEventUpdateDeathCountTMP : MonoBehaviour
{
    [SerializeField] private string onEventName = "";

    private TextMeshProUGUI _tmp;

    private int _deathCount = 0;    

    void Awake()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        UpdateTMP();
    }

    void OnEnable ()
    {
        EventManager.StartListening (onEventName, UpdateCount);
    }

    void OnDisable ()
    {
        EventManager.StopListening (onEventName, UpdateCount);
    }

    private void UpdateCount()
    {
        _deathCount ++;
        UpdateTMP();
    }

    private void UpdateTMP()
    {
        if (_tmp)
            _tmp.text = _deathCount.ToString();
    }
}
