
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
    
    [Header("Settings")]
    [SerializeField] private string activeOnEventName = "";
    [SerializeField] private string inactiveOnEventName = "";

    [Header("CCTV")]
    [SerializeField] List<GameObject> cameras;
    [SerializeField] int displayCameraIndex = 0;
    [SerializeField] float delayButtonPress = 0.5f;
    [SerializeField] GameObject CCTVNavigation;
    [SerializeField] TextMeshProUGUI currentCameraText;
    [SerializeField] GameObject passwordMenu;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] GameObject passwordError;
    [SerializeField] GameObject objectControlMenu;

    private bool _allowInput = false;
    private bool _arrowPressed = false;
    private bool _f2Pressed = false;
    private bool _enablePassword = false;
    private bool _enableLightSwitch = false;

    private bool _lightOn = true;

    private string _password = "ER28-0652";

    private string _userInput = "";

    private char[] _controlChars = {'\u0008', '\u0009', '\u0020', '\u001b','\u007F'};

    void OnEnable() {
        EventManager.StartListening (activeOnEventName, SetOn);
        EventManager.StartListening (inactiveOnEventName, SetOff);
    }

    void OnDisable() {
        EventManager.StopListening (activeOnEventName, SetOn);
        EventManager.StopListening (inactiveOnEventName, SetOff);
    }

    private void SetOn() 
    {
        SetCameraActive(displayCameraIndex, true);
        DisplayCamInfo();
        EnableInput();
    }

    private void SetOff()
    {
        PlayerController _playerController = FindObjectOfType<PlayerController>();
        DisableInput();
        if (_playerController) {
            _playerController.disablePlayerInput = false;
        }
        SetCameraActive(displayCameraIndex, false);
        displayCameraIndex = 0;
    }

    private void EnableInput()
    {
        _allowInput = true;
        Keyboard.current.onTextInput += OnTextInput;
    }

    private void DisableInput()
    {
        _allowInput = false;
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
                objectControlMenu.GetComponentInChildren<TextMeshProUGUI>().SetText("Open Door: F1");
                objectControlMenu.SetActive(true);
                break;
            case "Morgue":
                _enableLightSwitch = true;
                objectControlMenu.GetComponentInChildren<TextMeshProUGUI>().SetText("Toggle Light: F2");
                objectControlMenu.SetActive(true);
                break;
            default:
                passwordMenu.SetActive(false);
                objectControlMenu.SetActive(false);
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

    private void SetCameraActive(int index, bool active)
    {
        cameras[index].SetActive(active);
    } 

    private IEnumerator ChangeCamera(bool increment) {
        _arrowPressed = true;
        int next = GetNextIndex(increment);
        SetCameraActive(next, true);
        SetCameraActive(displayCameraIndex, false);
        displayCameraIndex = next;
        DisplayCamInfo();
        yield return new WaitForSeconds(delayButtonPress);
        _arrowPressed = false;
    }

    private IEnumerator ToggleLight() {
        _f2Pressed = true;
        _lightOn = !_lightOn;
        EventManager.TriggerEvent("PlayAudioSwitch");
        if (_lightOn)
            EventManager.TriggerEvent("LightOn");
        else 
            EventManager.TriggerEvent("LightOff");
        yield return new WaitForSeconds(delayButtonPress);
        _f2Pressed = false;
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
        if (_allowInput)
        {
            if (Keyboard.current.escapeKey.isPressed) {
                if (passwordMenu && passwordMenu.activeSelf) {
                    CCTVNavigation.SetActive(true);
                    objectControlMenu.SetActive(false);
                    passwordMenu.SetActive(false);
                } else {
                    Exit();
                }
            }
            else if (Keyboard.current.enterKey.isPressed || Keyboard.current.numpadEnterKey.isPressed)
            {
                if (passwordMenu && passwordMenu.activeSelf) {
                    if (CorrectPassword()) {
                        EventManager.TriggerEvent("OpenDoor");
                        CCTVNavigation.SetActive(true);
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
            else if (Keyboard.current.f1Key.isPressed && _enablePassword) {
                CCTVNavigation.SetActive(false);
                passwordMenu.SetActive(true);
                objectControlMenu.SetActive(false);
            }
            else if (Keyboard.current.f2Key.isPressed && _enableLightSwitch) {
                if (!_f2Pressed)
                    StartCoroutine(ToggleLight());
            }
            else if (Keyboard.current.backspaceKey.isPressed) {
                if (passwordMenu && passwordMenu.activeSelf) {
                    _userInput = _userInput.Remove(_userInput.Length - 1);
                    UpdateInputField();
                }
            }
        }
    }

    public void Exit()
    {
        EventManager.TriggerEvent(inactiveOnEventName);
    }
}
