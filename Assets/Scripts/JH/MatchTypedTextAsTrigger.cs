using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*  Listens for typed text and triggers ITriggers.
 *
 * - JH
 */
public class MatchTypedTextAsTrigger : MonoBehaviour
{
    [SerializeField] private string _text;
    private List<ITrigger> triggers;
    private int _position;

    private void Awake()
    {
        triggers = new List<ITrigger>(GetComponents<ITrigger>());
    }

    public void OnEnable()
    {
        Keyboard.current.onTextInput += OnTextInput;
    }

    public void OnDisable()
    {
        Keyboard.current.onTextInput -= OnTextInput;
    }

    private void OnTextInput(char ch)
    {
        // check if match can happen
        if (!AllowMatch()) return;


        if (_text[_position] == ch)
        {
            ++_position;
            if (_position == _text.Length)
            {
                Debug.Log($"Code entered {_text} entered");
                _position = 0;
                triggers?.ForEach(c => c.Invoke());
            }
        }
        else
        {
            _position = 0;
        }
    }

    private bool AllowMatch()
    {
        return _text != null && _position < _text.Length;
    }
}