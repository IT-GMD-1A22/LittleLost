using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    // [SerializeField] private Animator animator;
    // private readonly int _animIdLevels = Animator.StringToHash("ShowLevels");

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelsMenu;

    void Start()
    {
        levelsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void PlayButton()
    {
        TutorialButton();
    }
    public void LevelButton()
    {
        ToggleMenu();
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void TutorialButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LittleTutorial");
    }
    public void KitchenButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LittleKitchen");
    }
    public void LivingRoomButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LittleLivingroom");
    }
    public void EvilButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LittleEvil");
    }
    public void BackButton()
    {
        ToggleMenu();
    }

    public void ToggleMenu() 
    {
        levelsMenu.SetActive(!levelsMenu.activeSelf);
        mainMenu.SetActive(!mainMenu.activeSelf);
    }
}
