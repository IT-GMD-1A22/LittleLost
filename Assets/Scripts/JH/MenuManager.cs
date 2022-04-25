using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Menu manager for an example menu
 */
public class MenuManager : MonoBehaviour
{
    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
