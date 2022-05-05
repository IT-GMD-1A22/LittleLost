using System.Collections;
using UnityEngine;

/*
 * Script for displaying a UI text when activating the TV trigger.
 *
 * - NK
 */
public class DisplayTextForTV : MonoBehaviour
{
    [SerializeField] private GameObject TextObject;
    [SerializeField] private int DelayBeforeDestroyed;
    
    // Start is called before the first frame update
    void Start()
    {
        TextObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextObject.SetActive(true);
            StartCoroutine(DisplayText());
        }
    }

    private IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(DelayBeforeDestroyed);
        Destroy(TextObject);
    }
}
