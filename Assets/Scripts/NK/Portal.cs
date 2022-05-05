using UnityEngine;


/*
 * Script to load the next scene when the player find the 'orb'.
 * - NK
 */
public class Portal : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LittleEvil");
        }
    }
}
