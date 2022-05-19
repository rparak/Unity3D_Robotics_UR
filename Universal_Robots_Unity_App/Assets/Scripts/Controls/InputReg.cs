using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class InputReg
{
    public Guid guid;
    public CursorLockMode curserMode = CursorLockMode.None;
    public List<InputActionReference> inputs;

    public void InputReconfigure()
    {
        Cursor.lockState = curserMode;
        foreach (InputActionReference action in inputs)
        {
            action.action.Enable();
        }
    }

    public InputReg()
    {
        inputs = new List<InputActionReference>();
        guid = new Guid();
    }
}
