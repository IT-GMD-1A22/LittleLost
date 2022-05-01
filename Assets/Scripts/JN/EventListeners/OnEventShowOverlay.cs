using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEventShowOverlay : MonoBehaviour
{
    [SerializeField] private string onEventName = "";
    [SerializeField] private GameObject overlay;
    [SerializeField] private bool showOverlayOnEvent = true;

    [SerializeField] private float delay = 0.0f;

    private void Awake()
    {
        if (overlay)
            overlay.SetActive(!showOverlayOnEvent);
    }
    

     void OnEnable ()
    {
        EventManager.StartListening (onEventName, EventRecieved);
    }

    void OnDisable ()
    {
        EventManager.StopListening (onEventName, EventRecieved);
    }

    private IEnumerator ShowOverlay()
    {
        yield return new WaitForSeconds(delay);
        if (overlay)
            overlay.SetActive(showOverlayOnEvent);
    }

    private void EventRecieved()
    {
        StartCoroutine(ShowOverlay());
    }

    public void LaunchMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
