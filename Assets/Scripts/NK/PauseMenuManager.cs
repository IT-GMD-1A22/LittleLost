using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public void ActivatePauseMenu()
    {
        Time.timeScale = 0;
    }

    public void DeactivatePauseMenu()
    {
        Time.timeScale = 1;
    }
}
