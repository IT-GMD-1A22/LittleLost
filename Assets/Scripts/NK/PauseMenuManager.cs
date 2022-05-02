using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that enables and disables the pause menu.
 * Guidance and inspiration from: samyam, YouTube
 * https://www.youtube.com/watch?v=9tsbUoFfAgo
 *
 * - NK
 */

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    
    private PauseAction _pauseAction;
    
    // Made static such it is global and can be checked in the PlayerController
    public static bool isPaused = false;

    private void Awake()
    {
        _pauseAction = new PauseAction();
    }

    private void OnEnable()
    {
        _pauseAction.Enable();
    }

    private void OnDisable()
    {
        _pauseAction.Disable();
    }

    private void Start()
    {
        _pauseAction.Pause.PauseGame.performed += _ => DeterminePause();
    }

    private void DeterminePause()
    {
        if (isPaused)
        {
            DeactivatePauseMenu();
        }
        else
        {
            ActivatePauseMenu();
        }
    }

    public void ActivatePauseMenu()
    {
        AudioListener.pause = true;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void DeactivatePauseMenu()
    {
        AudioListener.pause = false;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
