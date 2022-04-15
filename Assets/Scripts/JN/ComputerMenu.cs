
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ComputerMenu : MonoBehaviour
{   
    [Header("World")]
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject canvasCamera;
    [SerializeField] GameObject screen;
    [SerializeField] GameObject screenLight;
    [SerializeField] GameObject screenCamera;
    [SerializeField] Animator doorAnimator; 

    
    [Header("CCTV")]
    [SerializeField] List<GameObject> cameras;
    [SerializeField] int displayCameraIndex = 0;
    [SerializeField] float delayChangeCamera = 0.5f;
    [SerializeField] GameObject cameraControlMenu;
    [SerializeField] TextMeshProUGUI currentCameraText;
    [SerializeField] GameObject passwordMenu;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] GameObject passwordError;

    private PlayerController _controller;
    private bool _arrowPressed = false;
    private bool _enablePassword = false;

    private string _password = "ER28-0652";

    private string _userInput = "";

    private char[] _controlChars = {'\u0008', '\u0009', '\u0020', '\u001b','\u007F'};

    void Start() {
        _controller = FindObjectOfType<PlayerController>();
        if (cameras.Any()) {
            cameras[displayCameraIndex].SetActive(true);
            DisplayCamInfo();
        } 
    }

    protected void OnEnable() {
        Keyboard.current.onTextInput += OnTextInput;
    }

    protected void OnDisable() {
        Keyboard.current.onTextInput -= OnTextInput;
        _userInput = "";
        passwordError.SetActive(false);
        UpdateInputField();
    }

    private void DisplayCamInfo() 
    {
        if (currentCameraText) {
            currentCameraText.SetText(cameras[displayCameraIndex].tag);
        }
        switch (cameras[displayCameraIndex].tag) {
            case "Morgue Door":
                _enablePassword = true;
                cameraControlMenu.GetComponentInChildren<TextMeshProUGUI>().SetText("Open Door: F1");
                cameraControlMenu.SetActive(true);
                break;
            default:
                passwordMenu.SetActive(false);
                cameraControlMenu.SetActive(false);
                break;
        }
    }

    private int GetNextIndex(bool increment) {
        int next;
        if (increment) {
            next = displayCameraIndex + 1;
            if (next == cameras.Count) {
                next = 0;
            }
        }
        else {
            next = displayCameraIndex - 1;
            if (next < 0) {
                next = cameras.Count - 1;
            }
        }
        return next;
    }

    private IEnumerator ChangeCamera(bool increment) {
        _arrowPressed = true;
        int next = GetNextIndex(increment);
        cameras[next].SetActive(true);
        cameras[displayCameraIndex].SetActive(false);
        displayCameraIndex = next;
        DisplayCamInfo();
        yield return new WaitForSeconds(delayChangeCamera);
        _arrowPressed = false;
    }

    private void OpenDoor() 
    {
        if (doorAnimator)
            doorAnimator.SetBool("Open", true);
    }

    private bool CorrectPassword() 
    {
        if (_userInput.Trim().Equals(_password)) {
            return true;
        }
        passwordError.SetActive(true);
        return false;
    }

    private void UpdateInputField() 
    {
        passwordError.SetActive(false);
        passwordInput.text = _userInput;
    }

    private void OnTextInput(char c) 
    {
        if (!_controlChars.Contains(c) && _enablePassword) {
            _userInput += c;
            UpdateInputField();
        }
    }

    void Update() 
    {
        if (Keyboard.current.escapeKey.isPressed) {
            if (passwordMenu && passwordMenu.activeSelf) {
                passwordMenu.SetActive(false);
            } else {
                Exit();
            }
        }
        else if (Keyboard.current.enterKey.isPressed || Keyboard.current.numpadEnterKey.isPressed)
        {
            if (passwordMenu && passwordMenu.activeSelf) {
                if (CorrectPassword()) {
                    OpenDoor();
                    passwordMenu.SetActive(false);  
                }
            }
        }
        else if (Keyboard.current.leftArrowKey.isPressed  && !passwordMenu.activeSelf) {
            if (!_arrowPressed)
                StartCoroutine(ChangeCamera(false));
        }
        else if (Keyboard.current.rightArrowKey.isPressed && !passwordMenu.activeSelf) {
            if (!_arrowPressed)
                StartCoroutine(ChangeCamera(true));
        }
        else if (Keyboard.current.f1Key.isPressed) {
            passwordMenu.SetActive(true);
        }
        else if (Keyboard.current.backspaceKey.isPressed) {
            if (passwordMenu && passwordMenu.activeSelf) {
                _userInput = _userInput.Remove(_userInput.Length - 1);
                UpdateInputField();
            }
        }
    }

    public void Exit()
    {
        if (canvas) {
            canvas.SetActive(false);
        }    
        if (canvasCamera) {
            canvasCamera.SetActive(false);
        }
        if (screen) {
            screen.SetActive(false);
        }
        if (screenLight) {
            screenLight.SetActive(false);
        }
        if (screenCamera) {
            screenCamera.SetActive(false);
        }    
        if (_controller) {
            _controller.disablePlayerInput = false;
        }
    }
}
