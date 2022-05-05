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
    private PlayerInputActionAsset _playerInputActionAsset;
    
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
        _playerInputActionAsset = GetComponent<PlayerInputActionAsset>();
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
    
    /*
     * This section below is a modified version from the inspiration to accommodate the use case in this project.
     */

    public void ActivatePauseMenu()
    {
        AudioListener.pause = true;
        isPaused = true;
        pauseMenu.SetActive(true);
        _playerInputActionAsset.cursorLocked = false;
    }

    public void DeactivatePauseMenu()
    {
        AudioListener.pause = false;
        isPaused = false;
        pauseMenu.SetActive(false);
        _playerInputActionAsset.cursorLocked = true;
    }

    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        DeactivatePauseMenu();
    }
}
