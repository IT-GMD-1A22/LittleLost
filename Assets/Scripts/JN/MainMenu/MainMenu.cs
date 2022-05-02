using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [Header("Levels Menu Items")]
    [SerializeField] private GameObject tutorialButton;
    [SerializeField] private GameObject kitchenButton;
    [SerializeField] private GameObject livingroomButton;
    [SerializeField] private GameObject evilButton;
    [SerializeField] private GameObject backButton;
    
    [Header("Animators")]
    [SerializeField] private Animator mainMenuAnimator;
    [SerializeField] private Animator levelsMenuAnimator;
    [SerializeField] private static string animatorBoolName = "Show";
    
    private readonly int _animId = Animator.StringToHash(animatorBoolName);
    private bool _firstLevelsView = true;
    
    void Start()
    {
        ShowMainButtons(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LaunchTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LittleTutorial");
    }

    public void LaunchKitchen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DinnerScene");
    }

    public void LaunchLivingRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TelevisionScene");
    }

    public void LaunchEvil()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LittleEvil");
    }

    public void Back()
    {
        ShowLevelsButtons(false);
        ShowMainButtons(true);
    }

    public void Levels()
    {
        if (_firstLevelsView) 
        {
            SetLevelButtonsActive(true);
            _firstLevelsView = false;
        }
        ShowMainButtons(false);
        ShowLevelsButtons(true);
    }

    private void ShowMainButtons(bool state)
    {
        if (mainMenuAnimator)
            mainMenuAnimator.SetBool(_animId, state);
    }
    
    private void ShowLevelsButtons(bool state)
    {
        if (levelsMenuAnimator)
            levelsMenuAnimator.SetBool(_animId, state);
    }

    private void SetLevelButtonsActive(bool state)
    {
        if (tutorialButton && kitchenButton && livingroomButton && evilButton && backButton)
        {
            tutorialButton.SetActive(state);
            kitchenButton.SetActive(state);
            livingroomButton.SetActive(state);
            evilButton.SetActive(state);
            backButton.SetActive(state);
        }
    }
}
